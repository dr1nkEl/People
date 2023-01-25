using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace People.Infrastructure.Saml;

/// <summary>
/// Class for working with saml response.
/// </summary>
internal class SamlResponse
{
    private XmlDocument xmlDocument;
    private XmlNamespaceManager xmlNameSpaceManager;
    private readonly X509Certificate2 certificate;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="certificate">SAML certificate.</param>
    /// <param name="samlResponse">Saml response from identity provider.</param>
    public SamlResponse(string certificate, string samlResponse)
    {
        xmlDocument = new XmlDocument();
        xmlDocument.PreserveWhitespace = true;
        xmlDocument.XmlResolver = null;
        xmlDocument.LoadXml(Encoding.UTF8.GetString(Convert.FromBase64String(samlResponse)));
        xmlNameSpaceManager = GetNamespaceManager();
        this.certificate = ConvertStringCertificateToX509Certificate2(certificate);

    }

    /// <summary>
    /// Check the validity of the response.
    /// </summary>
    /// <returns>Validation result.</returns>
    public bool IsValid()
    {
        var nodeList = xmlDocument.SelectNodes("//ds:Signature", xmlNameSpaceManager);

        var signedXml = new SignedXml(xmlDocument);

        if (nodeList.Count == 0)
        {
            return false;
        }

        signedXml.LoadXml((XmlElement)nodeList[0]);

        var isValidSignatureReference = IsValidSignatureReference(signedXml);
        var checkSignatureResult = signedXml.CheckSignature(certificate, true);
        var isExpired = IsExpired();
        return isValidSignatureReference && checkSignatureResult && !isExpired;
    }

    /// <summary>
    /// Get user email.
    /// </summary>
    /// <returns>Email.</returns>
    public string GetEmail()
    {
        return GetCustomAttribute("email");
    }

    /// <summary>
    /// Get user first name.
    /// </summary>
    /// <returns>First name.</returns>
    public string GetFirstName()
    {
        return GetCustomAttribute("first");
    }

    /// <summary>
    /// Get user last name.
    /// </summary>
    /// <returns>Last name.</returns>
    public string GetLastName()
    {
        return GetCustomAttribute("last");
    }

    private string GetCustomAttribute(string attr)
    {
        var node = xmlDocument.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='" + attr + "']/saml:AttributeValue", xmlNameSpaceManager);
        return node == null ? null : node.InnerText;
    }

    private XmlNamespaceManager GetNamespaceManager()
    {
        var manager = new XmlNamespaceManager(xmlDocument.NameTable);
        manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
        manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
        manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

        return manager;
    }

    private X509Certificate2 ConvertStringCertificateToX509Certificate2(string strCertificate)
    {
        byte[] bytes = new byte[strCertificate.Length];
        for (int i = 0; i < strCertificate.Length; i++)
        {
            bytes[i] = (byte)strCertificate[i];
        }

        return new X509Certificate2(bytes);
    }

    private bool IsValidSignatureReference(SignedXml signedXml)
    {
        if (signedXml.SignedInfo.References.Count != 1)
        {
            return false;
        }

        var reference = (Reference)signedXml.SignedInfo.References[0];
        var id = reference.Uri.Substring(1);

        var idElement = signedXml.GetIdElement(xmlDocument, id);

        if (idElement == xmlDocument.DocumentElement)
        {
            return true;
        }
        else
        {
            var assertionNode = xmlDocument.SelectSingleNode("/samlp:Response/saml:Assertion", xmlNameSpaceManager) as XmlElement;
            if (assertionNode != idElement)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsExpired()
    {
        var expirationDate = DateTime.MaxValue;
        var node = xmlDocument.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:SubjectConfirmation/saml:SubjectConfirmationData", xmlNameSpaceManager);
        if (node != null && node.Attributes["NotOnOrAfter"] != null)
        {
            DateTime.TryParse(node.Attributes["NotOnOrAfter"].Value, out expirationDate);
        }
        return DateTime.UtcNow > expirationDate.ToUniversalTime();
    }
}
