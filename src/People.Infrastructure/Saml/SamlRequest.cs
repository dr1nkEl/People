using System.IO.Compression;
using System.Text;
using System.Web;
using System.Xml;

namespace People.Infrastructure.Saml;

/// <summary>
/// Class for created url for redirect to saml service.
/// </summary>
internal class SamlRequest
{
    private string id;
    private string issueInstant;
    private string siteDomain;
    private string consumerEndpoint;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="siteDomain">Site domain.</param>
    /// <param name="consumerEndpoint">Consumer endpoint.</param>
    public SamlRequest(string siteDomain, string consumerEndpoint)
    {
        this.id = "_" + Guid.NewGuid().ToString();
        this.issueInstant = DateTime.Now.ToUniversalTime()
            .ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);

        this.siteDomain = siteDomain;
        this.consumerEndpoint = consumerEndpoint;
    }

    /// <summary>
    /// Method for created redirected url to saml service.
    /// </summary>
    /// <param name="samlEndpoint">Saml endpoint for authorization.</param>
    /// <param name="relayState"></param>
    /// <returns>Url for redirect.</returns>
    public string GetRedirectUrl(string samlEndpoint)
    {
        var url = samlEndpoint + "?SAMLRequest=" + HttpUtility.UrlEncode(GetRequest());

        return url;
    }

    private string GetRequest()
    {
        using StringWriter sw = new StringWriter();
        XmlWriterSettings xws = new XmlWriterSettings()
        {
            OmitXmlDeclaration = true
        };

        using (XmlWriter xw = XmlWriter.Create(sw, xws))
        {
            xw.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
            xw.WriteAttributeString("ID", id);
            xw.WriteAttributeString("Version", "2.0");
            xw.WriteAttributeString("IssueInstant", issueInstant);
            xw.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
            xw.WriteAttributeString("AssertionConsumerServiceURL", consumerEndpoint);
            xw.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
            xw.WriteString(siteDomain);
            xw.WriteEndElement();
            xw.WriteStartElement("samlp", "NameIDPolicy", "urn:oasis:names:tc:SAML:2.0:protocol");
            xw.WriteAttributeString("Format", "urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified");
            xw.WriteAttributeString("AllowCreate", "true");
            xw.WriteEndElement();
            xw.WriteEndElement();
        }

        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(new DeflateStream(memoryStream, CompressionMode.Compress, true),
            new UTF8Encoding(false));
        writer.Write(sw.ToString());
        writer.Close();

        string result = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length,
            Base64FormattingOptions.None);
        return result;
    }
}
