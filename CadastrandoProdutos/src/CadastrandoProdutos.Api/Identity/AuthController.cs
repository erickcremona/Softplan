﻿using CadastrandoProdutos.Api.Main;
using CadastrandoProdutos.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CadastroProdutos.Domain.Interfaces;
using CadastrandoProdutos.Api.ViewModels;

namespace CadastrandoProdutos.Api.Identity
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly INotificador _notificador;
        public AuthController(INotificador notificador,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings) : base(notificador)
        {
            _notificador = notificador;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }


        [ClaimsAuthorize("Auth", "ListarUsuarios")]
        [HttpGet]
        public async Task<ActionResult<List<IdentityUser>>> ObterUsuarios()
        {
            return await _userManager.Users.ToListAsync();
        }


        [ClaimsAuthorize("Auth", "ObterUsuario")]
        [HttpGet("{email}")]
        public async Task<ActionResult<LoginResponseViewModel>> ObterUsuarioPorEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return NotFound();

            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var userToken = new UserTokenViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
            };

            return CustomResponse(userToken);
        }

        [ClaimsAuthorize("Auth", "Claims")]
        [HttpPost("claims")]
        public async Task<ActionResult<UserTokenViewModel>> AdicionarClaims(UserClaimViewModel userClaim)
        {
            var user = await _userManager.FindByEmailAsync(userClaim.Email);

            if (user == null) return NotFound();

            if (userClaim.Claims.Count() < 1) NotificarErro("Informe pelo menos 1 Claim");

            if (!_notificador.TemNotificacao())
            {
                var claimsExistentes = await _userManager.GetClaimsAsync(user);

                foreach (var claim in userClaim.Claims)
                {
                    var claimExistente = claimsExistentes.SingleOrDefault(p => p.Type == claim.Type);

                    if (claimExistente != null)
                        await _userManager.RemoveClaimsAsync(user, claimsExistentes);

                    await _userManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value));
                }

                var userToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = userClaim.Claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                };

                return CustomResponse(userToken);
            }

            return CustomResponse();
        }

        [AllowAnonymous]
        [HttpPost("registro")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                if (user.Email == "admin@teste.com" && registerUser.Password == "Teste@123")
                {
                    await _userManager.AddClaimAsync(user, new Claim("Auth", "ListarUsuarios; ObterUsuario; Claims"));
                    await _userManager.AddClaimAsync(user, new Claim("Jogo", "Adicionar; Alterar; Excluir; ObterTodos; ObterPorId"));
                }
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GerarJwt(user.Email));
            }
            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(registerUser);
        }


        [AllowAnonymous]
        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJwt(loginUser.Email));
            }
            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou Senha incorretos");
            return CustomResponse(loginUser);
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
