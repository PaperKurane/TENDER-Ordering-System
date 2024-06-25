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

namespace TENDER_POS_System
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Tender")]
	public partial class TenderConnDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertItem_Category(Item_Category instance);
    partial void UpdateItem_Category(Item_Category instance);
    partial void DeleteItem_Category(Item_Category instance);
    partial void InsertMenu_Item(Menu_Item instance);
    partial void UpdateMenu_Item(Menu_Item instance);
    partial void DeleteMenu_Item(Menu_Item instance);
    partial void InsertOrder(Order instance);
    partial void UpdateOrder(Order instance);
    partial void DeleteOrder(Order instance);
    partial void InsertOrder_Item(Order_Item instance);
    partial void UpdateOrder_Item(Order_Item instance);
    partial void DeleteOrder_Item(Order_Item instance);
    #endregion
		
		public TenderConnDataContext() : 
				base(global::TENDER_POS_System.Properties.Settings.Default.TenderConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public TenderConnDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TenderConnDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TenderConnDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TenderConnDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Item_Category> Item_Categories
		{
			get
			{
				return this.GetTable<Item_Category>();
			}
		}
		
		public System.Data.Linq.Table<Menu_Item> Menu_Items
		{
			get
			{
				return this.GetTable<Menu_Item>();
			}
		}
		
		public System.Data.Linq.Table<Order> Orders
		{
			get
			{
				return this.GetTable<Order>();
			}
		}
		
		public System.Data.Linq.Table<Order_Item> Order_Items
		{
			get
			{
				return this.GetTable<Order_Item>();
			}
		}
		
		public System.Data.Linq.Table<OrderSummary> OrderSummaries
		{
			get
			{
				return this.GetTable<OrderSummary>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspUpdatePicturePath")]
		public int uspUpdatePicturePath([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(100)")] string userPicturePath)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userName, userPicturePath);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.AddMenuItem")]
		public int AddMenuItem([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_Name", DbType="VarChar(100)")] string item_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Category_ID", DbType="VarChar(100)")] string category_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_Description", DbType="VarChar(100)")] string item_Description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_Price", DbType="Int")] System.Nullable<int> item_Price, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_Image", DbType="VarChar(100)")] string item_Image)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), item_Name, category_ID, item_Description, item_Price, item_Image);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.UpdateMenuItem")]
		public int UpdateMenuItem([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_ID", DbType="Int")] System.Nullable<int> item_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_Name", DbType="VarChar(100)")] string item_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Category_ID", DbType="VarChar(100)")] string category_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_Description", DbType="VarChar(100)")] string item_Description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_Price", DbType="Int")] System.Nullable<int> item_Price, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_Image", DbType="VarChar(100)")] string item_Image)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), item_ID, item_Name, category_ID, item_Description, item_Price, item_Image);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertOrder")]
		public int InsertOrder([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Order_Date", DbType="DateTime")] System.Nullable<System.DateTime> order_Date, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Total_Amount", DbType="Int")] System.Nullable<int> total_Amount, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Order_ID", DbType="Int")] ref System.Nullable<int> order_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), order_Date, total_Amount, order_ID);
			order_ID = ((System.Nullable<int>)(result.GetParameterValue(2)));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertOrderItem")]
		public int InsertOrderItem([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Order_ID", DbType="Int")] System.Nullable<int> order_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Item_ID", DbType="VarChar(100)")] string item_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Quantity", DbType="Int")] System.Nullable<int> quantity, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Price", DbType="Int")] System.Nullable<int> price)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), order_ID, item_ID, quantity, price);
			return ((int)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Item_Category")]
	public partial class Item_Category : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Category_ID;
		
		private string _Category_Name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCategory_IDChanging(string value);
    partial void OnCategory_IDChanged();
    partial void OnCategory_NameChanging(string value);
    partial void OnCategory_NameChanged();
    #endregion
		
		public Item_Category()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Category_ID", DbType="VarChar(100) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Category_ID
		{
			get
			{
				return this._Category_ID;
			}
			set
			{
				if ((this._Category_ID != value))
				{
					this.OnCategory_IDChanging(value);
					this.SendPropertyChanging();
					this._Category_ID = value;
					this.SendPropertyChanged("Category_ID");
					this.OnCategory_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Category_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Category_Name
		{
			get
			{
				return this._Category_Name;
			}
			set
			{
				if ((this._Category_Name != value))
				{
					this.OnCategory_NameChanging(value);
					this.SendPropertyChanging();
					this._Category_Name = value;
					this.SendPropertyChanged("Category_Name");
					this.OnCategory_NameChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Menu_Item")]
	public partial class Menu_Item : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Item_ID;
		
		private string _Item_Name;
		
		private string _Category_ID;
		
		private string _Item_Description;
		
		private int _Item_Price;
		
		private string _Item_Image;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnItem_IDChanging(int value);
    partial void OnItem_IDChanged();
    partial void OnItem_NameChanging(string value);
    partial void OnItem_NameChanged();
    partial void OnCategory_IDChanging(string value);
    partial void OnCategory_IDChanged();
    partial void OnItem_DescriptionChanging(string value);
    partial void OnItem_DescriptionChanged();
    partial void OnItem_PriceChanging(int value);
    partial void OnItem_PriceChanged();
    partial void OnItem_ImageChanging(string value);
    partial void OnItem_ImageChanged();
    #endregion
		
		public Menu_Item()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Item_ID
		{
			get
			{
				return this._Item_ID;
			}
			set
			{
				if ((this._Item_ID != value))
				{
					this.OnItem_IDChanging(value);
					this.SendPropertyChanging();
					this._Item_ID = value;
					this.SendPropertyChanged("Item_ID");
					this.OnItem_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_Name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Item_Name
		{
			get
			{
				return this._Item_Name;
			}
			set
			{
				if ((this._Item_Name != value))
				{
					this.OnItem_NameChanging(value);
					this.SendPropertyChanging();
					this._Item_Name = value;
					this.SendPropertyChanged("Item_Name");
					this.OnItem_NameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Category_ID", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Category_ID
		{
			get
			{
				return this._Category_ID;
			}
			set
			{
				if ((this._Category_ID != value))
				{
					this.OnCategory_IDChanging(value);
					this.SendPropertyChanging();
					this._Category_ID = value;
					this.SendPropertyChanged("Category_ID");
					this.OnCategory_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_Description", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Item_Description
		{
			get
			{
				return this._Item_Description;
			}
			set
			{
				if ((this._Item_Description != value))
				{
					this.OnItem_DescriptionChanging(value);
					this.SendPropertyChanging();
					this._Item_Description = value;
					this.SendPropertyChanged("Item_Description");
					this.OnItem_DescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_Price", DbType="Int NOT NULL")]
		public int Item_Price
		{
			get
			{
				return this._Item_Price;
			}
			set
			{
				if ((this._Item_Price != value))
				{
					this.OnItem_PriceChanging(value);
					this.SendPropertyChanging();
					this._Item_Price = value;
					this.SendPropertyChanged("Item_Price");
					this.OnItem_PriceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_Image", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Item_Image
		{
			get
			{
				return this._Item_Image;
			}
			set
			{
				if ((this._Item_Image != value))
				{
					this.OnItem_ImageChanging(value);
					this.SendPropertyChanging();
					this._Item_Image = value;
					this.SendPropertyChanged("Item_Image");
					this.OnItem_ImageChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[Order]")]
	public partial class Order : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Order_ID;
		
		private System.DateTime _Order_Date;
		
		private int _Total_Amount;
		
		private EntitySet<Order_Item> _Order_Items;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOrder_IDChanging(int value);
    partial void OnOrder_IDChanged();
    partial void OnOrder_DateChanging(System.DateTime value);
    partial void OnOrder_DateChanged();
    partial void OnTotal_AmountChanging(int value);
    partial void OnTotal_AmountChanged();
    #endregion
		
		public Order()
		{
			this._Order_Items = new EntitySet<Order_Item>(new Action<Order_Item>(this.attach_Order_Items), new Action<Order_Item>(this.detach_Order_Items));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Order_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Order_ID
		{
			get
			{
				return this._Order_ID;
			}
			set
			{
				if ((this._Order_ID != value))
				{
					this.OnOrder_IDChanging(value);
					this.SendPropertyChanging();
					this._Order_ID = value;
					this.SendPropertyChanged("Order_ID");
					this.OnOrder_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Order_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Order_Date
		{
			get
			{
				return this._Order_Date;
			}
			set
			{
				if ((this._Order_Date != value))
				{
					this.OnOrder_DateChanging(value);
					this.SendPropertyChanging();
					this._Order_Date = value;
					this.SendPropertyChanged("Order_Date");
					this.OnOrder_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Total_Amount", DbType="Int NOT NULL")]
		public int Total_Amount
		{
			get
			{
				return this._Total_Amount;
			}
			set
			{
				if ((this._Total_Amount != value))
				{
					this.OnTotal_AmountChanging(value);
					this.SendPropertyChanging();
					this._Total_Amount = value;
					this.SendPropertyChanged("Total_Amount");
					this.OnTotal_AmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Order_Order_Item", Storage="_Order_Items", ThisKey="Order_ID", OtherKey="Order_ID")]
		public EntitySet<Order_Item> Order_Items
		{
			get
			{
				return this._Order_Items;
			}
			set
			{
				this._Order_Items.Assign(value);
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
		
		private void attach_Order_Items(Order_Item entity)
		{
			this.SendPropertyChanging();
			entity.Order = this;
		}
		
		private void detach_Order_Items(Order_Item entity)
		{
			this.SendPropertyChanging();
			entity.Order = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Order_Item")]
	public partial class Order_Item : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Order_Item_ID;
		
		private int _Order_ID;
		
		private string _Item_ID;
		
		private int _Quantity;
		
		private int _Price;
		
		private EntityRef<Order> _Order;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOrder_Item_IDChanging(int value);
    partial void OnOrder_Item_IDChanged();
    partial void OnOrder_IDChanging(int value);
    partial void OnOrder_IDChanged();
    partial void OnItem_IDChanging(string value);
    partial void OnItem_IDChanged();
    partial void OnQuantityChanging(int value);
    partial void OnQuantityChanged();
    partial void OnPriceChanging(int value);
    partial void OnPriceChanged();
    #endregion
		
		public Order_Item()
		{
			this._Order = default(EntityRef<Order>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Order_Item_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Order_Item_ID
		{
			get
			{
				return this._Order_Item_ID;
			}
			set
			{
				if ((this._Order_Item_ID != value))
				{
					this.OnOrder_Item_IDChanging(value);
					this.SendPropertyChanging();
					this._Order_Item_ID = value;
					this.SendPropertyChanged("Order_Item_ID");
					this.OnOrder_Item_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Order_ID", DbType="Int NOT NULL")]
		public int Order_ID
		{
			get
			{
				return this._Order_ID;
			}
			set
			{
				if ((this._Order_ID != value))
				{
					if (this._Order.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnOrder_IDChanging(value);
					this.SendPropertyChanging();
					this._Order_ID = value;
					this.SendPropertyChanged("Order_ID");
					this.OnOrder_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_ID", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Item_ID
		{
			get
			{
				return this._Item_ID;
			}
			set
			{
				if ((this._Item_ID != value))
				{
					this.OnItem_IDChanging(value);
					this.SendPropertyChanging();
					this._Item_ID = value;
					this.SendPropertyChanged("Item_ID");
					this.OnItem_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Quantity", DbType="Int NOT NULL")]
		public int Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				if ((this._Quantity != value))
				{
					this.OnQuantityChanging(value);
					this.SendPropertyChanging();
					this._Quantity = value;
					this.SendPropertyChanged("Quantity");
					this.OnQuantityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Price", DbType="Int NOT NULL")]
		public int Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if ((this._Price != value))
				{
					this.OnPriceChanging(value);
					this.SendPropertyChanging();
					this._Price = value;
					this.SendPropertyChanged("Price");
					this.OnPriceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Order_Order_Item", Storage="_Order", ThisKey="Order_ID", OtherKey="Order_ID", IsForeignKey=true)]
		public Order Order
		{
			get
			{
				return this._Order.Entity;
			}
			set
			{
				Order previousValue = this._Order.Entity;
				if (((previousValue != value) 
							|| (this._Order.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Order.Entity = null;
						previousValue.Order_Items.Remove(this);
					}
					this._Order.Entity = value;
					if ((value != null))
					{
						value.Order_Items.Add(this);
						this._Order_ID = value.Order_ID;
					}
					else
					{
						this._Order_ID = default(int);
					}
					this.SendPropertyChanged("Order");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.OrderSummary")]
	public partial class OrderSummary
	{
		
		private int _Order_Item_ID;
		
		private int _Order_ID;
		
		private string _Item_Name;
		
		private int _Quantity;
		
		private int _Price;
		
		private System.DateTime _Order_Date;
		
		public OrderSummary()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Order_Item_ID", DbType="Int NOT NULL")]
		public int Order_Item_ID
		{
			get
			{
				return this._Order_Item_ID;
			}
			set
			{
				if ((this._Order_Item_ID != value))
				{
					this._Order_Item_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Order_ID", DbType="Int NOT NULL")]
		public int Order_ID
		{
			get
			{
				return this._Order_ID;
			}
			set
			{
				if ((this._Order_ID != value))
				{
					this._Order_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_Name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Item_Name
		{
			get
			{
				return this._Item_Name;
			}
			set
			{
				if ((this._Item_Name != value))
				{
					this._Item_Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Quantity", DbType="Int NOT NULL")]
		public int Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				if ((this._Quantity != value))
				{
					this._Quantity = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Price", DbType="Int NOT NULL")]
		public int Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if ((this._Price != value))
				{
					this._Price = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Order_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Order_Date
		{
			get
			{
				return this._Order_Date;
			}
			set
			{
				if ((this._Order_Date != value))
				{
					this._Order_Date = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
