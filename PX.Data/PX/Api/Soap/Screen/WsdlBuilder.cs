// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.WsdlBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Services.Description;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;

#nullable disable
namespace PX.Api.Soap.Screen;

public class WsdlBuilder : IHttpHandler, IRequiresSessionState
{
  private string _screenID;

  public WsdlBuilder(string screenID) => this._screenID = screenID;

  public bool IsReusable => false;

  public void ProcessRequest(HttpContext context)
  {
    using (this.GetLoginScope())
    {
      context.Response.ContentType = "text/xml";
      string url = new Uri(context.Request.GetWebsiteAuthority(), PXUrl.ToAbsoluteUrl(context.Request.AppRelativeCurrentExecutionFilePath)).ToString();
      WsdlBuilder.Write(context.Response.Output, this._screenID, url);
      context.Response.End();
    }
  }

  protected virtual PXLoginScope GetLoginScope()
  {
    PXLoginScope loginScope = (PXLoginScope) null;
    if (PXContext.PXIdentity.IsAnonymous())
    {
      if (PXDatabase.Companies.Length != 0)
      {
        if (PXDatabase.Companies.Length == 1 || string.IsNullOrEmpty(this._screenID))
        {
          loginScope = new PXLoginScope("admin@" + PXDatabase.Companies[0], Array.Empty<string>());
        }
        else
        {
          int index;
          for (index = 0; index < PXDatabase.Companies.Length; ++index)
          {
            using (new PXLoginScope("admin@" + PXDatabase.Companies[index], PXAccess.GetAdministratorRoles()))
            {
              using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<SYWebService>(new PXDataField("WSDL"), (PXDataField) new PXDataFieldValue("ServiceID", PXDbType.VarChar, new int?(15), (object) this._screenID)))
              {
                PXDatabase.ResetCredentials();
                if (pxDataRecord != null)
                  break;
              }
            }
          }
          loginScope = new PXLoginScope("admin@" + PXDatabase.Companies[index % PXDatabase.Companies.Length], PXAccess.GetAdministratorRoles());
        }
      }
      else
        loginScope = new PXLoginScope("admin", PXAccess.GetAdministratorRoles());
    }
    return loginScope;
  }

  public static string GetWsdl(string screenID)
  {
    if (!string.IsNullOrEmpty(screenID))
      screenID = screenID.ToUpper();
    using (ScreenUtils.EnsureLogin())
    {
      ScreenUtils.ScreenInfo.Get(screenID);
      using (StringWriter output = new StringWriter())
      {
        WsdlBuilder.Write((TextWriter) output, screenID, $"http://localhost/Site/Soap/{screenID}.asmx");
        return output.ToString();
      }
    }
  }

  private static void Write(TextWriter output, string screenID, string url)
  {
    if (string.IsNullOrEmpty(screenID))
    {
      ServiceDescriptionReflector descriptionReflector = new ServiceDescriptionReflector();
      descriptionReflector.Reflect(typeof (ScreenUntyped), url);
      descriptionReflector.ServiceDescriptions[0].Write(output);
    }
    else if (PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID) != null)
    {
      ServiceDescriptionReflector descriptionReflector = new ServiceDescriptionReflector();
      descriptionReflector.Reflect(typeof (ScreenTyped), url);
      WsdlBuilder.Remove(descriptionReflector.ServiceDescriptions[0].Types.Schemas[0]);
      WsdlBuilder.Append(descriptionReflector.ServiceDescriptions[0].Types.Schemas[0], ScreenUtils.GetScreenInfo(screenID, false, false, true));
      descriptionReflector.ServiceDescriptions[0].Write(output);
    }
    else
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<SYWebService>(new PXDataField("WSDL"), (PXDataField) new PXDataFieldValue("ServiceID", PXDbType.VarChar, new int?(15), (object) screenID)))
      {
        if (pxDataRecord != null)
        {
          string str1 = pxDataRecord.GetString(0);
          if (!string.IsNullOrEmpty(str1))
          {
            string str2 = str1.Replace($"http://localhost/Site/Soap/{screenID}.asmx", url);
            output.Write(str2);
            return;
          }
        }
      }
      ServiceDescriptionReflector descriptionReflector = new ServiceDescriptionReflector();
      descriptionReflector.Reflect(typeof (ScreenGeneric), url);
      descriptionReflector.ServiceDescriptions[0].Write(output);
    }
  }

  public static void ProcessHelpRequest(HttpContext context)
  {
    try
    {
      Page compiledPageInstance = (Page) PageParser.GetCompiledPageInstance(HttpRuntime.AppDomainAppVirtualPath + "/DefaultWsdlHelpGenerator.aspx", HttpRuntime.AspInstallDirectory + "Config\\DefaultWsdlHelpGenerator.aspx", context);
      ServiceDescriptionCollection descriptionCollection = (ServiceDescriptionCollection) (context.Items[(object) "wsdlsWithPost"] ?? context.Items[(object) "wsdls"]);
      if (descriptionCollection != null && descriptionCollection.Count > 0 && descriptionCollection[0].Services.Count > 0 && (string.Equals(descriptionCollection[0].Services[0].ServiceDescription.TargetNamespace, "http://www.acumatica.com/typed/") || string.Equals(descriptionCollection[0].Services[0].ServiceDescription.TargetNamespace, "http://www.acumatica.com/untyped/") || string.Equals(descriptionCollection[0].Services[0].ServiceDescription.TargetNamespace, "http://www.acumatica.com/generic/")) && context.Request.Url.Segments.Length >= 2 && string.Equals("Soap/", context.Request.Url.Segments[context.Request.Url.Segments.Length - 2], StringComparison.OrdinalIgnoreCase))
      {
        string screenId = PXContext.GetScreenID();
        string newValue = context.Request.GetExternalUrl().ToString();
        if (!string.IsNullOrEmpty(context.Request.Url.Query))
          newValue = newValue.Replace(context.Request.Url.Query, "");
        if (!string.IsNullOrEmpty(screenId))
        {
          string upper = screenId.ToUpper();
          string wsdl = WsdlBuilder.GetWsdl(upper.Replace(".", ""));
          if (!string.IsNullOrEmpty(wsdl))
          {
            if (descriptionCollection[0].Bindings.Count > 2)
              wsdl = WsdlBuilder.AppendPost(wsdl, upper.Replace(".", ""));
            string s = wsdl.Replace($"http://localhost/Site/Soap/{upper.Replace(".", "")}.asmx", newValue);
            descriptionCollection.Clear();
            using (StringReader stringReader = new StringReader(s))
              descriptionCollection.Add(ServiceDescription.Read((TextReader) stringReader, true));
            descriptionCollection[0].Services[0].Name = upper;
          }
        }
        else
        {
          string segment = context.Request.Url.Segments[context.Request.Url.Segments.Length - 1];
          string str = segment.Substring(0, segment.Length - 5);
          if (!string.IsNullOrEmpty(str))
          {
            string upper = str.ToUpper();
            string wsdl = (string) null;
            for (int index = 0; index == 0 || index < PXDatabase.Companies.Length; ++index)
            {
              PXLoginScope pxLoginScope = (PXLoginScope) null;
              if (PXContext.PXIdentity.IsAnonymous())
                pxLoginScope = PXDatabase.Companies.Length == 0 ? new PXLoginScope("admin", PXAccess.GetAdministratorRoles()) : new PXLoginScope("admin@" + PXDatabase.Companies[index], PXAccess.GetAdministratorRoles());
              using (pxLoginScope)
              {
                using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<SYWebService>(new PXDataField("WSDL"), (PXDataField) new PXDataFieldValue("ServiceID", PXDbType.VarChar, new int?(15), (object) upper)))
                {
                  PXDatabase.ResetCredentials();
                  if (pxDataRecord != null)
                  {
                    wsdl = pxDataRecord.GetString(0);
                    break;
                  }
                }
              }
            }
            if (!string.IsNullOrEmpty(wsdl))
            {
              if (descriptionCollection[0].Bindings.Count > 2)
                wsdl = WsdlBuilder.AppendPost(wsdl, upper);
              string s = wsdl.Replace($"http://localhost/Site/Soap/{upper}.asmx", newValue);
              descriptionCollection.Clear();
              using (StringReader stringReader = new StringReader(s))
                descriptionCollection.Add(ServiceDescription.Read((TextReader) stringReader, true));
              descriptionCollection[0].Services[0].Name = upper;
            }
          }
        }
        using (ScreenUtils.EnsureLogin())
        {
          string str1 = PXMessages.LocalizeNoPrefix("Clears the underlying screen content");
          string str2 = PXMessages.LocalizeNoPrefix("Returns the status and the elapsed time of the process launched from the screen");
          string str3 = PXMessages.LocalizeNoPrefix("Retrieves the list of commands of the import or export scenario configured in the system");
          string str4 = PXMessages.LocalizeNoPrefix("Returns the predefined set of all commands available for the screen");
          string str5 = PXMessages.LocalizeNoPrefix("Forces the system to use the incoming set of commands instead of the predefined set in the screen");
          string str6 = PXMessages.LocalizeNoPrefix("Performs similarly to the Export by Scenario form, returning tabular data");
          string str7 = PXMessages.LocalizeNoPrefix("Performs similarly to the Import by Scenario form, accepting tabular data");
          string str8 = PXMessages.LocalizeNoPrefix("Allows simultaneous import and export of data in the form of commands coupled with values");
          string str9 = PXMessages.LocalizeNoPrefix("Logs in to the system");
          string str10 = PXMessages.LocalizeNoPrefix("Changes the business date");
          string str11 = PXMessages.LocalizeNoPrefix("Changes the interface language accepting the name, such as \"en-US\" or \"fr-CA\"");
          string str12 = PXMessages.LocalizeNoPrefix("Instructs the system to extend results with element descriptors in subsequent calls");
          foreach (PortType portType in (CollectionBase) descriptionCollection[0].PortTypes)
          {
            foreach (System.Web.Services.Description.Operation operation in (CollectionBase) portType.Operations)
            {
              if (operation.Name == "Login")
                operation.Documentation = str9;
              else if (operation.Name == "SetBusinessDate")
                operation.Documentation = str10;
              else if (operation.Name == "SetLocaleName")
                operation.Documentation = str11;
              else if (operation.Name == "SetSchemaMode")
                operation.Documentation = str12;
              else if (operation.Name == "GetScenario")
                operation.Documentation = str3;
              else if (operation.Name.EndsWith("Clear"))
                operation.Documentation = str1 + (operation.Name.Length == 13 ? Mask.Format(" (>CC.CC.CC.CC)", operation.Name.Substring(0, 8)) : "");
              else if (operation.Name.EndsWith("GetSchema"))
                operation.Documentation = str4 + (operation.Name.Length == 17 ? Mask.Format(" (>CC.CC.CC.CC)", operation.Name.Substring(0, 8)) : "");
              else if (operation.Name.EndsWith("SetSchema"))
                operation.Documentation = str5 + (operation.Name.Length == 17 ? Mask.Format(" (>CC.CC.CC.CC)", operation.Name.Substring(0, 8)) : "");
              else if (operation.Name.EndsWith("GetProcessStatus"))
                operation.Documentation = str2 + (operation.Name.Length == 24 ? Mask.Format(" (>CC.CC.CC.CC)", operation.Name.Substring(0, 8)) : "");
              else if (operation.Name.EndsWith("Import"))
                operation.Documentation = str7 + (operation.Name.Length == 14 ? Mask.Format(" (>CC.CC.CC.CC)", operation.Name.Substring(0, 8)) : "");
              else if (operation.Name.EndsWith("Export"))
                operation.Documentation = str6 + (operation.Name.Length == 14 ? Mask.Format(" (>CC.CC.CC.CC)", operation.Name.Substring(0, 8)) : "");
              else if (operation.Name.EndsWith("Submit"))
                operation.Documentation = str8 + (operation.Name.Length == 14 ? Mask.Format(" (>CC.CC.CC.CC)", operation.Name.Substring(0, 8)) : "");
            }
          }
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        compiledPageInstance.DataBinding += WsdlBuilder.\u003C\u003EO.\u003C0\u003E__SortMethods ?? (WsdlBuilder.\u003C\u003EO.\u003C0\u003E__SortMethods = new EventHandler(WsdlBuilder.SortMethods));
      }
      compiledPageInstance.ProcessRequest(context);
    }
    catch (InvalidOperationException ex)
    {
      context.Response.StatusCode = 400;
      context.Response.End();
    }
    catch (HttpRequestValidationException ex)
    {
      context.Response.StatusCode = 400;
      context.Response.End();
    }
  }

  private static void SortMethods(object sender, EventArgs e)
  {
    Repeater repeater = (Repeater) sender.GetType().GetField("MethodList", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sender);
    repeater.DataSource = (object) new SortedList((IDictionary) repeater.DataSource, (IComparer) new WsdlBuilder.MethodComparer());
  }

  private static string AppendPost(string wsdl, string screenID)
  {
    int startIndex1 = wsdl.IndexOf("<wsdl:portType", StringComparison.OrdinalIgnoreCase);
    if (startIndex1 != -1)
      wsdl = wsdl.Insert(startIndex1, "<wsdl:message name=\"GetScenarioHttpPostIn\"><wsdl:part name=\"scenario\" type=\"s:string\" /></wsdl:message><wsdl:message name=\"GetScenarioHttpPostOut\"><wsdl:part name=\"Body\" element=\"tns:ArrayOfCommand\" /></wsdl:message><wsdl:message name=\"LoginHttpPostIn\"><wsdl:part name=\"name\" type=\"s:string\" /><wsdl:part name=\"password\" type=\"s:string\" /></wsdl:message><wsdl:message name=\"LoginHttpPostOut\"><wsdl:part name=\"Body\" element=\"tns:LoginResult\" /></wsdl:message><wsdl:message name=\"SetBusinessDateHttpPostIn\"><wsdl:part name=\"date\" type=\"s:string\" /></wsdl:message><wsdl:message name=\"SetBusinessDateHttpPostOut\" /><wsdl:message name=\"SetLocaleNameHttpPostIn\"><wsdl:part name=\"localeName\" type=\"s:string\" /></wsdl:message><wsdl:message name=\"SetLocaleNameHttpPostOut\" /><wsdl:message name=\"SetSchemaModeHttpPostIn\"><wsdl:part name=\"mode\" type=\"tns:SchemaMode\" /></wsdl:message><wsdl:message name=\"SetSchemaModeHttpPostOut\" />");
    int startIndex2 = wsdl.IndexOf("<wsdl:binding", startIndex1, StringComparison.OrdinalIgnoreCase);
    if (startIndex2 != -1)
      wsdl = wsdl.Insert(startIndex2, "<wsdl:portType name=\"ScreenHttpPost\"><wsdl:operation name=\"GetScenario\"><wsdl:input message=\"tns:GetScenarioHttpPostIn\" /><wsdl:output message=\"tns:GetScenarioHttpPostOut\" /></wsdl:operation><wsdl:operation name=\"Login\"><wsdl:input message=\"tns:LoginHttpPostIn\" /><wsdl:output message=\"tns:LoginHttpPostOut\" /></wsdl:operation><wsdl:operation name=\"SetBusinessDate\"><wsdl:input message=\"tns:SetBusinessDateHttpPostIn\" /><wsdl:output message=\"tns:SetBusinessDateHttpPostOut\" /></wsdl:operation><wsdl:operation name=\"SetLocaleName\"><wsdl:input message=\"tns:SetLocaleNameHttpPostIn\" /><wsdl:output message=\"tns:SetLocaleNameHttpPostOut\" /></wsdl:operation><wsdl:operation name=\"SetSchemaMode\"><wsdl:input message=\"tns:SetSchemaModeHttpPostIn\" /><wsdl:output message=\"tns:SetSchemaModeHttpPostOut\" /></wsdl:operation></wsdl:portType>");
    int startIndex3 = wsdl.IndexOf("<wsdl:service", startIndex2, StringComparison.OrdinalIgnoreCase);
    if (startIndex3 != -1)
      wsdl = wsdl.Insert(startIndex3, "<wsdl:binding name=\"ScreenHttpPost\" type=\"tns:ScreenHttpPost\"><http:binding verb=\"POST\" /><wsdl:operation name=\"GetScenario\"><http:operation location=\"/GetScenario\" /><wsdl:input><mime:content type=\"application/x-www-form-urlencoded\" /></wsdl:input><wsdl:output><mime:mimeXml part=\"Body\" /></wsdl:output></wsdl:operation><wsdl:operation name=\"Login\"><http:operation location=\"/Login\" /><wsdl:input><mime:content type=\"application/x-www-form-urlencoded\" /></wsdl:input><wsdl:output><mime:mimeXml part=\"Body\" /></wsdl:output></wsdl:operation><wsdl:operation name=\"SetBusinessDate\"><http:operation location=\"/SetBusinessDate\" /><wsdl:input><mime:content type=\"application/x-www-form-urlencoded\" /></wsdl:input><wsdl:output /></wsdl:operation><wsdl:operation name=\"SetLocaleName\"><http:operation location=\"/SetLocaleName\" /><wsdl:input><mime:content type=\"application/x-www-form-urlencoded\" /></wsdl:input><wsdl:output /></wsdl:operation><wsdl:operation name=\"SetSchemaMode\"><http:operation location=\"/SetSchemaMode\" /><wsdl:input><mime:content type=\"application/x-www-form-urlencoded\" /></wsdl:input><wsdl:output /></wsdl:operation></wsdl:binding>");
    int startIndex4 = wsdl.IndexOf("</wsdl:service", startIndex3, StringComparison.OrdinalIgnoreCase);
    if (startIndex4 != -1)
      wsdl = wsdl.Insert(startIndex4, $"<wsdl:port name=\"ScreenHttpPost\" binding=\"tns:ScreenHttpPost\"><http:address location=\"http://localhost/Site/Soap/{screenID}.asmx\" /></wsdl:port>");
    return wsdl;
  }

  private static void Remove(XmlSchema S)
  {
    List<XmlSchemaObject> xmlSchemaObjectList = new List<XmlSchemaObject>();
    foreach (XmlSchemaObject xmlSchemaObject1 in S.Items)
    {
      if (xmlSchemaObject1 is XmlSchemaComplexType)
      {
        if (((XmlSchemaType) xmlSchemaObject1).Name == "ArrayOfAction")
          xmlSchemaObjectList.Add(xmlSchemaObject1);
        else if (((XmlSchemaType) xmlSchemaObject1).Name == typeof (Command).Name)
        {
          XmlSchemaObject xmlSchemaObject2 = (XmlSchemaObject) null;
          XmlSchemaSequence particle = (XmlSchemaSequence) ((XmlSchemaComplexType) xmlSchemaObject1).Particle;
          foreach (XmlSchemaObject xmlSchemaObject3 in particle.Items)
          {
            if (((XmlSchemaElement) xmlSchemaObject3).Name == "Name")
            {
              xmlSchemaObject2 = xmlSchemaObject3;
              break;
            }
          }
          if (xmlSchemaObject2 != null)
            particle.Items.Remove(xmlSchemaObject2);
        }
      }
    }
    foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaObjectList)
      S.Items.Remove(xmlSchemaObject);
  }

  private static void Append(XmlSchema S, PX.Api.Models.Content screenInfo)
  {
    string ns = "http://www.acumatica.com/typed/";
    WsdlTypeBuilder wsdlTypeBuilder1 = new WsdlTypeBuilder()
    {
      Name = "Content",
      IsExisting = true
    };
    WsdlTypeBuilder wsdlTypeBuilder2 = new WsdlTypeBuilder()
    {
      Name = "Actions"
    };
    WsdlTypeBuilder wsdlTypeBuilder3 = new WsdlTypeBuilder()
    {
      Name = "ImportResult",
      IsExisting = true
    };
    WsdlTypeBuilder wsdlTypeBuilder4 = new WsdlTypeBuilder()
    {
      Name = "PrimaryKey"
    };
    if (screenInfo.Containers.Length != 0 && screenInfo.Containers[0].ServiceCommands != null && screenInfo.Containers[0].ServiceCommands.Length != 0)
    {
      bool flag = false;
      foreach (PX.Api.Models.Field field in screenInfo.Containers[0].Fields)
      {
        foreach (Command serviceCommand in screenInfo.Containers[0].ServiceCommands)
        {
          if (serviceCommand is Key && serviceCommand.FieldName == field.FieldName)
          {
            wsdlTypeBuilder4.AddField(field.Name, (System.Type) null).SchemaTypeName = new XmlQualifiedName(typeof (PX.Api.Models.Value).Name, ns);
            flag = true;
            break;
          }
        }
      }
      if (flag)
      {
        wsdlTypeBuilder4.Save(S);
        wsdlTypeBuilder3.AddField("Processed", typeof (bool)).IsValueType = true;
        wsdlTypeBuilder3.AddField("Error", typeof (string));
        wsdlTypeBuilder3.AddField("Keys", (System.Type) null).SchemaTypeName = new XmlQualifiedName("PrimaryKey", ns);
        wsdlTypeBuilder3.Save(S);
      }
    }
    foreach (PX.Api.Models.Action action in screenInfo.Actions)
      wsdlTypeBuilder2.AddField(action.Name, (System.Type) null).SchemaTypeName = new XmlQualifiedName(action.GetType().Name, ns);
    wsdlTypeBuilder2.Save(S);
    wsdlTypeBuilder1.AddField("Actions", (System.Type) null).SchemaTypeName = new XmlQualifiedName("Actions", ns);
    foreach (Container container in screenInfo.Containers)
    {
      WsdlTypeBuilder wsdlTypeBuilder5 = new WsdlTypeBuilder()
      {
        Name = container.Name
      };
      wsdlTypeBuilder5.AddField("DisplayName", typeof (string));
      foreach (PX.Api.Models.Field field in container.Fields)
      {
        if (!string.IsNullOrEmpty(field.Name))
          wsdlTypeBuilder5.AddField(field.Name, (System.Type) null).SchemaTypeName = new XmlQualifiedName(field.GetType().Name, ns);
      }
      WsdlTypeBuilder wsdlTypeBuilder6 = new WsdlTypeBuilder()
      {
        Name = container.Name + "ServiceCommands"
      };
      foreach (Command serviceCommand in container.ServiceCommands)
        wsdlTypeBuilder6.AddField(serviceCommand.Name, (System.Type) null).SchemaTypeName = new XmlQualifiedName(serviceCommand.GetType().Name, ns);
      wsdlTypeBuilder6.Save(S);
      wsdlTypeBuilder5.AddField("ServiceCommands", (System.Type) null).SchemaTypeName = new XmlQualifiedName(container.Name + "ServiceCommands", ns);
      wsdlTypeBuilder5.Save(S);
      wsdlTypeBuilder1.AddField(container.Name, (System.Type) null).SchemaTypeName = new XmlQualifiedName(container.Name, ns);
    }
    wsdlTypeBuilder1.Save(S);
  }

  private sealed class MethodComparer : IComparer
  {
    private static string[] order = new string[20]
    {
      "Login",
      "Logout",
      "SetLocaleName",
      "SetBusinessDate",
      "SetSchemaMode",
      "GetSchema",
      "SetSchema",
      "Import",
      "Export",
      "Submit",
      "Clear",
      "GetProcessStatus",
      "GetScenario",
      "UntypedGetSchema",
      "UntypedSetSchema",
      "UntypedImport",
      "UntypedExport",
      "UntypedSubmit",
      "UntypedClear",
      "UntypedGetProcessStatus"
    };

    public int Compare(object x, object y)
    {
      string str = (string) x;
      string strB = (string) y;
      bool flag1 = false;
      bool flag2 = false;
      int num1 = Array.IndexOf<string>(WsdlBuilder.MethodComparer.order, str);
      if (num1 == -1)
      {
        num1 = Array.IndexOf<string>(WsdlBuilder.MethodComparer.order, str.Substring(8));
        flag1 = true;
      }
      int num2 = Array.IndexOf<string>(WsdlBuilder.MethodComparer.order, strB);
      if (num2 == -1)
      {
        num2 = Array.IndexOf<string>(WsdlBuilder.MethodComparer.order, strB.Substring(8));
        flag2 = true;
      }
      if (flag1)
      {
        if (!flag2)
          return 1;
        return str.Substring(0, 8) == strB.Substring(0, 8) ? num1.CompareTo(num2) : str.CompareTo(strB);
      }
      return flag2 ? -1 : num1.CompareTo(num2);
    }
  }
}
