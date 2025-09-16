// Decompiled with JetBrains decompiler
// Type: PX.Data.PXShortCutAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Export;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class PXShortCutAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  private static readonly IDictionary<System.Type, IDictionary<string, PXShortCutAttribute>> _commands = (IDictionary<System.Type, IDictionary<string, PXShortCutAttribute>>) new Dictionary<System.Type, IDictionary<string, PXShortCutAttribute>>();
  private readonly HotKeyInfo _shortcut;

  static PXShortCutAttribute()
  {
    try
    {
      foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies(nameof (PXShortCutAttribute)))
      {
        if (!(enumTypesInAssembly != (System.Type) null) || !enumTypesInAssembly.IsGenericType && !enumTypesInAssembly.IsAbstract && typeof (PXGraph).IsAssignableFrom(enumTypesInAssembly))
        {
          IDictionary<string, PXShortCutAttribute> dictionary = (IDictionary<string, PXShortCutAttribute>) null;
          foreach (MethodInfo method in enumTypesInAssembly.GetMethods())
          {
            ParameterInfo[] parameters;
            if (typeof (IEnumerable).IsAssignableFrom(method.ReturnType) && (parameters = method.GetParameters()) != null && parameters.Length != 0 && typeof (PXAdapter).IsAssignableFrom(parameters[0].ParameterType))
            {
              object[] customAttributes = method.GetCustomAttributes(typeof (PXShortCutAttribute), false);
              if (customAttributes != null && customAttributes.Length != 0)
              {
                PXSubstManager.AddTypeToNamedList(nameof (PXShortCutAttribute), enumTypesInAssembly);
                if (dictionary == null)
                {
                  if (!PXShortCutAttribute._commands.TryGetValue(enumTypesInAssembly, out dictionary))
                    PXShortCutAttribute._commands.Add(enumTypesInAssembly, dictionary = (IDictionary<string, PXShortCutAttribute>) new Dictionary<string, PXShortCutAttribute>());
                  dictionary.Clear();
                }
                foreach (PXShortCutAttribute shortCutAttribute in customAttributes)
                {
                  dictionary.Remove(method.Name);
                  dictionary.Add(method.Name, shortCutAttribute);
                }
              }
            }
          }
        }
      }
      PXSubstManager.SaveTypeCache(nameof (PXShortCutAttribute));
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

  private PXShortCutAttribute(bool ctrl, bool shift, bool alt, int keyCode, int[] charCodes)
  {
    this._shortcut = new HotKeyInfo(ctrl, shift, alt, keyCode, charCodes);
  }

  public PXShortCutAttribute(bool ctrl, bool shift, bool alt, KeyCodes key)
    : this(ctrl, shift, alt, (int) key, (int[]) null)
  {
  }

  public PXShortCutAttribute(bool ctrl, bool shift, bool alt, params char[] chars)
    : this(ctrl, shift, alt, 0, HotKeyInfo.ConvertChars(chars))
  {
  }

  public static IEnumerable<KeyValuePair<string, PXShortCutAttribute>> GetDeclared(string graphName)
  {
    System.Type type = PXBuildManager.GetType(graphName, false);
    IDictionary<string, PXShortCutAttribute> dictionary;
    if (type != (System.Type) null && PXShortCutAttribute._commands.TryGetValue(type, out dictionary))
    {
      foreach (KeyValuePair<string, PXShortCutAttribute> keyValuePair in (IEnumerable<KeyValuePair<string, PXShortCutAttribute>>) dictionary)
        yield return keyValuePair;
    }
  }

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) PXButtonState.CreateInstance(e.ReturnState, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(), PXConfirmationType.Unspecified, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, this, (System.Type) null);
  }

  /// <summary>Get.</summary>
  public HotKeyInfo HotKey => this._shortcut;
}
