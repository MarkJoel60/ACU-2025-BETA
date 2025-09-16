// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXSpecialTagParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXSpecialTagParser : PXBlockParser
{
  private static readonly Dictionary<string, System.Type> _registeredTagParsers = new Dictionary<string, System.Type>();
  private static readonly Dictionary<string, System.Type> _registeredTagElements = new Dictionary<string, System.Type>();

  static PXSpecialTagParser()
  {
    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
    {
      try
      {
        if (PXSubstManager.IsSuitableTypeExportAssembly(assembly))
        {
          if (!assembly.FullName.StartsWith("App_SubCode_"))
          {
            System.Type[] typeArray = (System.Type[]) null;
            try
            {
              if (!assembly.IsDynamic)
                typeArray = assembly.GetExportedTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
              typeArray = ex.Types;
            }
            if (typeArray != null)
            {
              foreach (System.Type type in typeArray)
              {
                if (type != (System.Type) null && !type.IsGenericType && !type.IsAbstract)
                {
                  if (typeof (PXSpecialTagParser).IsAssignableFrom(type) && type != typeof (PXSpecialTagParser))
                    PXSpecialTagParser.TryAddTagHandler(type, (IDictionary<string, System.Type>) PXSpecialTagParser._registeredTagParsers);
                  else if (typeof (PXSpecialTagElement).IsAssignableFrom(type) && type != typeof (PXSpecialTagElement))
                    PXSpecialTagParser.TryAddTagHandler(type, (IDictionary<string, System.Type>) PXSpecialTagParser._registeredTagElements);
                }
              }
            }
          }
        }
      }
      catch (StackOverflowException ex)
      {
        throw;
      }
      catch (OutOfMemoryException ex)
      {
        throw;
      }
      catch
      {
      }
    }
  }

  private static void TryAddTagHandler(System.Type type, IDictionary<string, System.Type> handlerCollection)
  {
    if (!(Attribute.GetCustomAttribute((MemberInfo) type, typeof (PXSpecialTagParser.TagNameAttribute)) is PXSpecialTagParser.TagNameAttribute customAttribute) || !(type.GetConstructor(new System.Type[0]) != (ConstructorInfo) null))
      return;
    foreach (string name in customAttribute.Names)
      handlerCollection.Add(name, type);
  }

  public static void RegisterTagHandler(string tagName, System.Type type)
  {
    if (!typeof (PXSpecialTagParser).IsAssignableFrom(type) || type == typeof (PXSpecialTagParser))
      return;
    if (PXSpecialTagParser._registeredTagParsers.ContainsKey(tagName))
      PXSpecialTagParser._registeredTagParsers[tagName] = type;
    else
      PXSpecialTagParser._registeredTagParsers.Add(tagName, type);
  }

  public static void RegisterTagElement(string tagName, System.Type type)
  {
    if (!typeof (PXSpecialTagElement).IsAssignableFrom(type) || type == typeof (PXSpecialTagElement))
      return;
    if (PXSpecialTagParser._registeredTagElements.ContainsKey(tagName))
      PXSpecialTagParser._registeredTagElements[tagName] = type;
    else
      PXSpecialTagParser._registeredTagElements.Add(tagName, type);
  }

  protected virtual void DoParse(
    PXBlockParser.ParseContext context,
    WikiArticle result,
    params string[] parameters)
  {
  }

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    string empty = string.Empty;
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken == Token.chars || nextToken == Token.specialtagend)
      {
        empty += TokenValue;
        if (nextToken == Token.specialtagend)
        {
          string[] sourceArray = empty.TrimStart('{').TrimEnd('}').Split(' ');
          if (sourceArray.Length == 0)
            break;
          string lower = sourceArray[0].ToLower();
          string[] destinationArray = new string[sourceArray.Length - 1];
          if (destinationArray.Length != 0)
            Array.Copy((Array) sourceArray, 1, (Array) destinationArray, 0, destinationArray.Length);
          if (PXSpecialTagParser._registeredTagParsers.ContainsKey(lower))
          {
            ((PXSpecialTagParser) Activator.CreateInstance(PXSpecialTagParser._registeredTagParsers[lower])).DoParse(context, result, destinationArray);
            break;
          }
          PXSpecialTagElement elem = PXSpecialTagParser._registeredTagElements.ContainsKey(lower) ? (PXSpecialTagElement) Activator.CreateInstance(PXSpecialTagParser._registeredTagElements[lower]) : (PXSpecialTagElement) new PXCommonSpecialTagElement($"{{{lower}}}");
          if (result.Current is PXParagraphElement)
            this.AddElementToParagraph((PXElement) elem, context, result);
          else
            result.AddElement((PXElement) elem);
          elem.Props = destinationArray;
          break;
        }
      }
      else
      {
        context.StartIndex -= TokenValue.Length;
        break;
      }
    }
  }

  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public sealed class TagNameAttribute : Attribute
  {
    private readonly string[] _names;

    public TagNameAttribute(params string[] names) => this._names = names;

    public string[] Names => this._names;
  }
}
