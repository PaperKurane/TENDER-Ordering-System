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
				base(global::TENDER_POS_System.Properties.Settings.Default.TenderConnectionString, mappingSource)
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
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Item_Category")]
	public partial class Item_Category : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Category_ID;
		
		private string _Category_Name;
		
		private EntitySet<Menu_Item> _Menu_Items;
		
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
			this._Menu_Items = new EntitySet<Menu_Item>(new Action<Menu_Item>(this.attach_Menu_Items), new Action<Menu_Item>(this.detach_Menu_Items));
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Item_Category_Menu_Item", Storage="_Menu_Items", ThisKey="Category_ID", OtherKey="Category_ID")]
		public EntitySet<Menu_Item> Menu_Items
		{
			get
			{
				return this._Menu_Items;
			}
			set
			{
				this._Menu_Items.Assign(value);
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
		
		private void attach_Menu_Items(Menu_Item entity)
		{
			this.SendPropertyChanging();
			entity.Item_Category = this;
		}
		
		private void detach_Menu_Items(Menu_Item entity)
		{
			this.SendPropertyChanging();
			entity.Item_Category = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Menu_Item")]
	public partial class Menu_Item : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Item_ID;
		
		private string _Item_Name;
		
		private string _Category_ID;
		
		private int _Item_Price;
		
		private string _Item_Image;
		
		private EntitySet<Order_Item> _Order_Items;
		
		private EntityRef<Item_Category> _Item_Category;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnItem_IDChanging(string value);
    partial void OnItem_IDChanged();
    partial void OnItem_NameChanging(string value);
    partial void OnItem_NameChanged();
    partial void OnCategory_IDChanging(string value);
    partial void OnCategory_IDChanged();
    partial void OnItem_PriceChanging(int value);
    partial void OnItem_PriceChanged();
    partial void OnItem_ImageChanging(string value);
    partial void OnItem_ImageChanged();
    #endregion
		
		public Menu_Item()
		{
			this._Order_Items = new EntitySet<Order_Item>(new Action<Order_Item>(this.attach_Order_Items), new Action<Order_Item>(this.detach_Order_Items));
			this._Item_Category = default(EntityRef<Item_Category>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_ID", DbType="VarChar(100) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
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
					if (this._Item_Category.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCategory_IDChanging(value);
					this.SendPropertyChanging();
					this._Category_ID = value;
					this.SendPropertyChanged("Category_ID");
					this.OnCategory_IDChanged();
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Menu_Item_Order_Item", Storage="_Order_Items", ThisKey="Item_ID", OtherKey="Item_ID")]
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Item_Category_Menu_Item", Storage="_Item_Category", ThisKey="Category_ID", OtherKey="Category_ID", IsForeignKey=true)]
		public Item_Category Item_Category
		{
			get
			{
				return this._Item_Category.Entity;
			}
			set
			{
				Item_Category previousValue = this._Item_Category.Entity;
				if (((previousValue != value) 
							|| (this._Item_Category.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Item_Category.Entity = null;
						previousValue.Menu_Items.Remove(this);
					}
					this._Item_Category.Entity = value;
					if ((value != null))
					{
						value.Menu_Items.Add(this);
						this._Category_ID = value.Category_ID;
					}
					else
					{
						this._Category_ID = default(string);
					}
					this.SendPropertyChanged("Item_Category");
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
		
		private void attach_Order_Items(Order_Item entity)
		{
			this.SendPropertyChanging();
			entity.Menu_Item = this;
		}
		
		private void detach_Order_Items(Order_Item entity)
		{
			this.SendPropertyChanging();
			entity.Menu_Item = null;
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
		
		private EntityRef<Menu_Item> _Menu_Item;
		
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
			this._Menu_Item = default(EntityRef<Menu_Item>);
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
					if (this._Menu_Item.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Menu_Item_Order_Item", Storage="_Menu_Item", ThisKey="Item_ID", OtherKey="Item_ID", IsForeignKey=true)]
		public Menu_Item Menu_Item
		{
			get
			{
				return this._Menu_Item.Entity;
			}
			set
			{
				Menu_Item previousValue = this._Menu_Item.Entity;
				if (((previousValue != value) 
							|| (this._Menu_Item.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Menu_Item.Entity = null;
						previousValue.Order_Items.Remove(this);
					}
					this._Menu_Item.Entity = value;
					if ((value != null))
					{
						value.Order_Items.Add(this);
						this._Item_ID = value.Item_ID;
					}
					else
					{
						this._Item_ID = default(string);
					}
					this.SendPropertyChanged("Menu_Item");
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
}
#pragma warning restore 1591
