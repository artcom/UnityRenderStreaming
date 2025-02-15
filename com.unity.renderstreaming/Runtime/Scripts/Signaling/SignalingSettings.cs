using System;
using System.Collections.Generic;
using System.Linq;
using Unity.WebRTC;
using UnityEngine;

namespace Unity.RenderStreaming
{
    internal sealed class SignalingSettingsAttribute : PropertyAttribute { }

    /// <summary>
    ///
    /// </summary>
    public enum IceCredentialType
    {
        /// <summary>
        ///
        /// </summary>
        Password = 0,

        /// <summary>
        ///
        /// </summary>
        OAuth = 1
    }

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class IceServer
    {
        /// <summary>
        ///
        /// </summary>
        public IReadOnlyCollection<string> urls => m_urls;

        /// <summary>
        ///
        /// </summary>
        public string username => m_username;

        /// <summary>
        ///
        /// </summary>
        public IceCredentialType credentialType => m_credentialType;

        /// <summary>
        ///
        /// </summary>
        public string credential => m_credential;

        [SerializeField]
        private string[] m_urls;
        [SerializeField]
        private string m_username;
        [SerializeField]
        private IceCredentialType m_credentialType;
        [SerializeField]
        private string m_credential;

        /// <summary>
        ///
        /// </summary>
        /// <param name="server"></param>
        public static implicit operator RTCIceServer(IceServer server)
        {
            var iceServer = new RTCIceServer
            {
                urls = server.urls.ToArray(),
                username = server.username,
                credential = server.credential,
                credentialType = (RTCIceCredentialType)server.credentialType
            };
            return iceServer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IceServer Clone()
        {
            return new IceServer(this.urls.ToArray(), this.username, this.credentialType, this.credential);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="username"></param>
        /// <param name="credentialType"></param>
        /// <param name="credential"></param>
        public IceServer(string[] urls = null, string username = null, IceCredentialType credentialType = IceCredentialType.Password, string credential = null)
        {
            m_urls = urls?.ToArray();
            m_username = username;
            m_credential = credential;
            m_credentialType = credentialType;
        }

        internal IceServer(RTCIceServer server)
        {
            m_urls = server.urls.ToArray();
            m_username = server.username;
            m_credential = server.credential;
            m_credentialType = (IceCredentialType)server.credentialType;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public abstract class SignalingSettings
    {
        /// <summary>
        ///
        /// </summary>
        public abstract IReadOnlyCollection<IceServer> iceServers { get; }

        /// <summary>
        ///
        /// </summary>
        public abstract Type signalingClass { get; }
    }
}
