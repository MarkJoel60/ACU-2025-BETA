// Decompiled with JetBrains decompiler
// Type: PX.Api.PXVersionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

#nullable disable
namespace PX.Api;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class PXVersionAttribute : Attribute
{
  public static readonly string[] SkippedAssemblies = new string[3]
  {
    "DotNetOpenAuth",
    "Microsoft.Data.Services.Client",
    "Azure.Core"
  };
  private static readonly Lazy<Dictionary<string, Type[]>> _services = new Lazy<Dictionary<string, Type[]>>((Func<Dictionary<string, Type[]>>) (() => PXVersionAttribute.GetVersionedTypes().GroupBy<KeyValuePair<string, Type>, string>((Func<KeyValuePair<string, Type>, string>) (c => c.Key)).ToDictionary<IGrouping<string, KeyValuePair<string, Type>>, string, Type[]>((Func<IGrouping<string, KeyValuePair<string, Type>>, string>) (c => c.Key), (Func<IGrouping<string, KeyValuePair<string, Type>>, Type[]>) (c => c.Select<KeyValuePair<string, Type>, Type>((Func<KeyValuePair<string, Type>, Type>) (v => v.Value)).ToArray<Type>()))), LazyThreadSafetyMode.PublicationOnly);

  public string Version { get; private set; }

  public string Name { get; private set; }

  public PXVersionAttribute()
  {
    this.Version = (string) null;
    this.Name = (string) null;
  }

  public PXVersionAttribute(string version, string name)
    : this(version)
  {
    this.Name = name;
  }

  public PXVersionAttribute(string version) => this.Version = version;

  internal static IEnumerable<KeyValuePair<string, Type>> GetVersionedTypes(
    bool withName = true,
    bool withVersion = true)
  {
    Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
    for (int index = 0; index < assemblyArray.Length; ++index)
    {
      Assembly assembly = assemblyArray[index];
      if (!((IEnumerable<string>) PXVersionAttribute.SkippedAssemblies).Any<string>((Func<string, bool>) (a => assembly.GetName().Name.StartsWith(a))))
      {
        Type[] source;
        try
        {
          source = assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
          source = ((IEnumerable<Type>) ex.Types).Where<Type>((Func<Type, bool>) (t => t != (Type) null)).ToArray<Type>();
        }
        foreach (var data in ((IEnumerable<Type>) source).SelectMany(type => type.GetCustomAttributes(typeof (PXVersionAttribute), false).Cast<PXVersionAttribute>().Select(a => new
        {
          attribute = a,
          type = type
        })).Where(tuple =>
        {
          if (withName && string.IsNullOrWhiteSpace(tuple.attribute.Name))
            return false;
          return !withVersion || !string.IsNullOrWhiteSpace(tuple.attribute.Version);
        }))
          yield return new KeyValuePair<string, Type>(PXVersionAttribute.GetKey(data.attribute.Version, data.attribute.Name), data.type);
      }
    }
    assemblyArray = (Assembly[]) null;
  }

  public static Dictionary<string, Type[]> Services => PXVersionAttribute._services.Value;

  public static string GetKey(string version, string name) => $"{version}%{name}";

  public static string GetKey(Type type)
  {
    PXVersionAttribute versionAttribute = (PXVersionAttribute) ((IEnumerable<object>) type.GetCustomAttributes(typeof (PXVersionAttribute), false)).Single<object>();
    return PXVersionAttribute.GetKey(versionAttribute.Version, versionAttribute.Name);
  }
}
