// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.ForeignDAC`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Search;

[PXInternalUseOnly]
public abstract class ForeignDAC<TDac> where TDac : IBqlTable
{
  public abstract class GetField<TField> where TField : IBqlField
  {
    public abstract class WithQuery<TQuery> where TQuery : BqlCommand, IBqlSelect<TDac>
    {
      public class AndParameter<TParameter> : IForeignDacFieldRetrievalInfo where TParameter : IBqlField
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[1]
        {
          typeof (TField)
        };

        public System.Type[] RequiredDacFields { get; } = new System.Type[1]
        {
          typeof (TParameter)
        };

        public System.Type Query => typeof (TQuery);
      }

      public class AndParameters<TParameter1, TParameter2> : IForeignDacFieldRetrievalInfo
        where TParameter1 : IBqlField
        where TParameter2 : IBqlField
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[1]
        {
          typeof (TField)
        };

        public System.Type[] RequiredDacFields { get; } = new System.Type[2]
        {
          typeof (TParameter1),
          typeof (TParameter2)
        };

        public System.Type Query => typeof (TQuery);
      }

      public class AndParameters<TParameters> : IForeignDacFieldRetrievalInfo where TParameters : TypeArrayOf<IBqlField>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[1]
        {
          typeof (TField)
        };

        public System.Type[] RequiredDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TParameters), (string) null);

        public System.Type Query => typeof (TQuery);
      }
    }

    public abstract class WithParameter<TParameter> where TParameter : IBqlField
    {
      public class AndQuery<TQuery> : IForeignDacFieldRetrievalInfo where TQuery : BqlCommand, IBqlSelect<TDac>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[1]
        {
          typeof (TField)
        };

        public System.Type[] RequiredDacFields { get; } = new System.Type[1]
        {
          typeof (TParameter)
        };

        public System.Type Query => typeof (TQuery);
      }
    }

    public abstract class WithParameters<TParameter1, TParameter2>
      where TParameter1 : IBqlField
      where TParameter2 : IBqlField
    {
      public class AndQuery<TQuery> : IForeignDacFieldRetrievalInfo where TQuery : BqlCommand, IBqlSelect<TDac>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[1]
        {
          typeof (TField)
        };

        public System.Type[] RequiredDacFields { get; } = new System.Type[2]
        {
          typeof (TParameter1),
          typeof (TParameter2)
        };

        public System.Type Query => typeof (TQuery);
      }
    }

    public abstract class WithParameters<TParameters> where TParameters : TypeArrayOf<IBqlField>
    {
      public class AndQuery<TQuery> : IForeignDacFieldRetrievalInfo where TQuery : BqlCommand, IBqlSelect<TDac>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[1]
        {
          typeof (TField)
        };

        public System.Type[] RequiredDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TParameters), (string) null);

        public System.Type Query => typeof (TQuery);
      }
    }
  }

  public abstract class GetFields<TField1, TField2>
    where TField1 : IBqlField
    where TField2 : IBqlField
  {
    public abstract class WithQuery<TQuery> where TQuery : BqlCommand, IBqlSelect<TDac>
    {
      public class AndParameter<TParameter> : IForeignDacFieldRetrievalInfo where TParameter : IBqlField
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[2]
        {
          typeof (TField1),
          typeof (TField2)
        };

        public System.Type[] RequiredDacFields { get; } = new System.Type[1]
        {
          typeof (TParameter)
        };

        public System.Type Query => typeof (TQuery);
      }

      public class AndParameters<TParameter1, TParameter2> : IForeignDacFieldRetrievalInfo
        where TParameter1 : IBqlField
        where TParameter2 : IBqlField
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[2]
        {
          typeof (TField1),
          typeof (TField2)
        };

        public System.Type[] RequiredDacFields { get; } = new System.Type[2]
        {
          typeof (TParameter1),
          typeof (TParameter2)
        };

        public System.Type Query => typeof (TQuery);
      }

      public class AndParameters<TParameters> : IForeignDacFieldRetrievalInfo where TParameters : TypeArrayOf<IBqlField>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[2]
        {
          typeof (TField1),
          typeof (TField2)
        };

        public System.Type[] RequiredDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TParameters), (string) null);

        public System.Type Query => typeof (TQuery);
      }
    }

    public abstract class WithParameter<TParameter> where TParameter : IBqlField
    {
      public class AndQuery<TQuery> : IForeignDacFieldRetrievalInfo where TQuery : BqlCommand, IBqlSelect<TDac>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[2]
        {
          typeof (TField1),
          typeof (TField2)
        };

        public System.Type[] RequiredDacFields { get; } = new System.Type[1]
        {
          typeof (TParameter)
        };

        public System.Type Query => typeof (TQuery);
      }
    }

    public abstract class WithParameters<TParameter1, TParameter2>
      where TParameter1 : IBqlField
      where TParameter2 : IBqlField
    {
      public class AndQuery<TQuery> : IForeignDacFieldRetrievalInfo where TQuery : BqlCommand, IBqlSelect<TDac>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[2]
        {
          typeof (TField1),
          typeof (TField2)
        };

        public System.Type[] RequiredDacFields { get; } = new System.Type[2]
        {
          typeof (TParameter1),
          typeof (TParameter2)
        };

        public System.Type Query => typeof (TQuery);
      }
    }

    public abstract class WithParameters<TParameters> where TParameters : TypeArrayOf<IBqlField>
    {
      public class AndQuery<TQuery> : IForeignDacFieldRetrievalInfo where TQuery : BqlCommand, IBqlSelect<TDac>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = new System.Type[2]
        {
          typeof (TField1),
          typeof (TField2)
        };

        public System.Type[] RequiredDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TParameters), (string) null);

        public System.Type Query => typeof (TQuery);
      }
    }
  }

  public abstract class GetFields<TFields> where TFields : TypeArrayOf<IBqlField>
  {
    public abstract class WithQuery<TQuery> where TQuery : BqlCommand, IBqlSelect<TDac>
    {
      public class AndParameter<TParameter> : IForeignDacFieldRetrievalInfo where TParameter : IBqlField
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TFields), (string) null);

        public System.Type[] RequiredDacFields { get; } = new System.Type[1]
        {
          typeof (TParameter)
        };

        public System.Type Query => typeof (TQuery);
      }

      public class AndParameters<TParameter1, TParameter2> : IForeignDacFieldRetrievalInfo
        where TParameter1 : IBqlField
        where TParameter2 : IBqlField
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TFields), (string) null);

        public System.Type[] RequiredDacFields { get; } = new System.Type[2]
        {
          typeof (TParameter1),
          typeof (TParameter2)
        };

        public System.Type Query => typeof (TQuery);
      }

      public class AndParameters<TParameters> : IForeignDacFieldRetrievalInfo where TParameters : TypeArrayOf<IBqlField>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TFields), (string) null);

        public System.Type[] RequiredDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TParameters), (string) null);

        public System.Type Query => typeof (TQuery);
      }
    }

    public abstract class WithParameter<TParameter> where TParameter : IBqlField
    {
      public class AndQuery<TQuery> : IForeignDacFieldRetrievalInfo where TQuery : BqlCommand, IBqlSelect<TDac>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TFields), (string) null);

        public System.Type[] RequiredDacFields { get; } = new System.Type[1]
        {
          typeof (TParameter)
        };

        public System.Type Query => typeof (TQuery);
      }
    }

    public abstract class WithParameters<TParameter1, TParameter2>
      where TParameter1 : IBqlField
      where TParameter2 : IBqlField
    {
      public class AndQuery<TQuery> : IForeignDacFieldRetrievalInfo where TQuery : BqlCommand, IBqlSelect<TDac>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TFields), (string) null);

        public System.Type[] RequiredDacFields { get; } = new System.Type[2]
        {
          typeof (TParameter1),
          typeof (TParameter2)
        };

        public System.Type Query => typeof (TQuery);
      }
    }

    public abstract class WithParameters<TParameters> where TParameters : TypeArrayOf<IBqlField>
    {
      public class AndQuery<TQuery> : IForeignDacFieldRetrievalInfo where TQuery : BqlCommand, IBqlSelect<TDac>
      {
        public System.Type ForeignDac => typeof (TDac);

        public System.Type[] ForeignDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TFields), (string) null);

        public System.Type[] RequiredDacFields { get; } = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TParameters), (string) null);

        public System.Type Query => typeof (TQuery);
      }
    }
  }
}
