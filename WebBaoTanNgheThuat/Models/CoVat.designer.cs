﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBaoTanNgheThuat.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="WebBaoTan")]
	public partial class CoVatDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCOVAT(COVAT instance);
    partial void UpdateCOVAT(COVAT instance);
    partial void DeleteCOVAT(COVAT instance);
    #endregion
		
		public CoVatDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["WebBaoTanConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CoVatDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CoVatDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CoVatDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CoVatDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<COVAT> COVATs
		{
			get
			{
				return this.GetTable<COVAT>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.COVAT")]
	public partial class COVAT : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _TEN;
		
		private string _SRC;
		
		private string _TEN_TAC_GIA;
		
		private string _NOI_DUNG;
		
		private string _THOI_GIAN;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTENChanging(string value);
    partial void OnTENChanged();
    partial void OnSRCChanging(string value);
    partial void OnSRCChanged();
    partial void OnTEN_TAC_GIAChanging(string value);
    partial void OnTEN_TAC_GIAChanged();
    partial void OnNOI_DUNGChanging(string value);
    partial void OnNOI_DUNGChanged();
    partial void OnTHOI_GIANChanging(string value);
    partial void OnTHOI_GIANChanged();
    #endregion
		
		public COVAT()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TEN", DbType="NVarChar(200)")]
		public string TEN
		{
			get
			{
				return this._TEN;
			}
			set
			{
				if ((this._TEN != value))
				{
					this.OnTENChanging(value);
					this.SendPropertyChanging();
					this._TEN = value;
					this.SendPropertyChanged("TEN");
					this.OnTENChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SRC", DbType="NVarChar(300) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string SRC
		{
			get
			{
				return this._SRC;
			}
			set
			{
				if ((this._SRC != value))
				{
					this.OnSRCChanging(value);
					this.SendPropertyChanging();
					this._SRC = value;
					this.SendPropertyChanged("SRC");
					this.OnSRCChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TEN_TAC_GIA", DbType="NVarChar(200)")]
		public string TEN_TAC_GIA
		{
			get
			{
				return this._TEN_TAC_GIA;
			}
			set
			{
				if ((this._TEN_TAC_GIA != value))
				{
					this.OnTEN_TAC_GIAChanging(value);
					this.SendPropertyChanging();
					this._TEN_TAC_GIA = value;
					this.SendPropertyChanged("TEN_TAC_GIA");
					this.OnTEN_TAC_GIAChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NOI_DUNG", DbType="NVarChar(3000)")]
		public string NOI_DUNG
		{
			get
			{
				return this._NOI_DUNG;
			}
			set
			{
				if ((this._NOI_DUNG != value))
				{
					this.OnNOI_DUNGChanging(value);
					this.SendPropertyChanging();
					this._NOI_DUNG = value;
					this.SendPropertyChanged("NOI_DUNG");
					this.OnNOI_DUNGChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_THOI_GIAN", DbType="NVarChar(200)")]
		public string THOI_GIAN
		{
			get
			{
				return this._THOI_GIAN;
			}
			set
			{
				if ((this._THOI_GIAN != value))
				{
					this.OnTHOI_GIANChanging(value);
					this.SendPropertyChanging();
					this._THOI_GIAN = value;
					this.SendPropertyChanged("THOI_GIAN");
					this.OnTHOI_GIANChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
