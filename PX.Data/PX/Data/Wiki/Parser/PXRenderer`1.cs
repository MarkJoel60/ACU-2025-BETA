// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXRenderer`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Represents a base rendering class with thread-safe capabilities.
/// </summary>
/// <typeparam name="T">PXRender-derived type.</typeparam>
public abstract class PXRenderer<T> : PXRenderer
{
  private static readonly Dictionary<System.Type, T> renderers = new Dictionary<System.Type, T>();
  private static ReaderWriterLock rwlock = new ReaderWriterLock();

  static PXRenderer() => PXRenderer<T>.RegisterDynamicRenderers();

  /// <summary>
  /// Registers a T-type renderer for specified element type.
  /// </summary>
  /// <param name="elementType">Type of element renderer is being registered for.</param>
  /// <param name="renderer">An object which should render 'elementType' objects.</param>
  protected static void RegisterRenderer(System.Type elementType, T renderer)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXRenderer<T>.rwlock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
      if ((object) PXRenderer<T>.GetRenderer(elementType) == null)
        PXRenderer<T>.renderers.Add(elementType, renderer);
      else
        PXRenderer<T>.renderers[elementType] = renderer;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  /// <summary>Returns renderer for the given element.</summary>
  /// <param name="elem">Element to find renderer for.</param>
  /// <returns>An PXHtmlRenderer object which is able to render 'elem' or null if appropriate renderer can not be found.</returns>
  protected T GetRenderer(PXElement elem)
  {
    return elem == null ? default (T) : PXRenderer<T>.GetRenderer(elem.GetType());
  }

  protected void RegisterCustomRenderers()
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXRenderer<T>.rwlock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
      this.OnRegisterCustomRenderers();
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  private static void RegisterDynamicRenderers()
  {
    try
    {
      foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies(nameof (RegisterDynamicRenderers)))
      {
        if (enumTypesInAssembly != (System.Type) null && !enumTypesInAssembly.IsGenericType && !enumTypesInAssembly.IsAbstract)
        {
          Attribute[] customAttributes = Attribute.GetCustomAttributes((MemberInfo) enumTypesInAssembly, typeof (PXElementRendererAttribute), true);
          if (customAttributes != null && customAttributes.Length != 0)
          {
            PXSubstManager.AddTypeToNamedList(nameof (RegisterDynamicRenderers), enumTypesInAssembly);
            foreach (PXElementRendererAttribute rendererAttribute in customAttributes)
            {
              if (typeof (T).IsAssignableFrom(rendererAttribute.Renderer))
              {
                T renderer = (T) rendererAttribute.CreateRenderer(enumTypesInAssembly);
                foreach (System.Type type in rendererAttribute.Types)
                  PXRenderer<T>.RegisterRenderer(type, renderer);
              }
            }
          }
        }
      }
      PXSubstManager.SaveTypeCache(nameof (RegisterDynamicRenderers));
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

  private static T GetRenderer(System.Type elemType)
  {
    return !PXRenderer<T>.renderers.ContainsKey(elemType) ? default (T) : PXRenderer<T>.renderers[elemType];
  }

  /// <summary>
  /// Registers custom renderers for PXElement-derived objects.
  /// Override this method to provide rendering of your custom objects.
  /// </summary>
  protected abstract void OnRegisterCustomRenderers();
}
