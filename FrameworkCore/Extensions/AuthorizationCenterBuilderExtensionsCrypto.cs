using System;
using System.Security.Cryptography.X509Certificates;
using FrameworkCore.Extensions.Store;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace FrameworkCore.Extensions
{
    /// <summary>
    ///     授权中心扩展加密
    /// </summary>
    public static class AuthorizationCenterBuilderExtensionsCrypto
    {
        /// <summary>
        ///     设置签名凭据.
        /// </summary>
        /// <param name="builder">builder.</param>
        /// <param name="certificate">证书.</param>
        /// <param name="signingAlgorithm">签名算法(默认为RS256)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">证书为null</exception>
        /// <exception cref="InvalidOperationException">X509证书没有私钥.</exception>
        public static IAuthorizationCenterBuild AddSigningCredential(this IAuthorizationCenterBuild builder,
            X509Certificate2 certificate, string signingAlgorithm = SecurityAlgorithms.RsaSha256Signature)
        {
            if (certificate == null) throw new ArgumentNullException(nameof(certificate));

            if (!certificate.HasPrivateKey) throw new InvalidOperationException("X509证书没有私钥。");

            // 添加签名算法名到密钥ID，允许对两个不同的算法(例如RS256和PS56)使用相同的密钥;
            var key = new X509SecurityKey(certificate);
            key.KeyId += signingAlgorithm;

            var credential = new SigningCredentials(key, signingAlgorithm);
            return builder.AddSigningCredential(credential);
        }

        /// <summary>
        ///     设置签名凭据.
        /// </summary>
        /// <param name="builder">builder.</param>
        /// <param name="credential">签名证书.</param>
        /// <returns></returns>
        public static IAuthorizationCenterBuild AddSigningCredential(this IAuthorizationCenterBuild builder,
            SigningCredentials credential)
        {
            //注册证书存储单例
            builder.Services.AddSingleton<ISigningCredentialStore>(new InMemorySigningCredentialsStore(credential));
            var keyInfo = new SecurityKeyInfo
            {
                Key = credential.Key,
                SigningAlgorithm = credential.Algorithm
            };
            builder.Services.AddSingleton<IValidationKeysStore>(new InMemoryValidationKeysStore(new[] {keyInfo}));
            return builder;
        }
    }
}