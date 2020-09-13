﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
		private  IProductEndpoint _productEndpoint;
		public SalesViewModel(IProductEndpoint productEndpoint)
		{
			_productEndpoint = productEndpoint;
		}

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducts();
		}

		private async Task LoadProducts()
		{
			var productList = await _productEndpoint.GetAll();
			Products = new BindingList<ProductModel>(productList);
		}

		private BindingList<ProductModel>	_products;

		public BindingList<ProductModel> Products
		{
			get { return _products; }
			set 
			{ 
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		private BindingList<ProductModel> _cart;

		public BindingList<ProductModel> Cart
		{
			get { return _cart; }
			set 
			{ 
				_cart = value;
				NotifyOfPropertyChange(() => Cart);
			}
		}

		private int _itemQuantity;

		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{ 
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
			}
		}

		public string SubTotal { 
			get 
			{
				//TODO - Replace with calculation
				return "$0.00";
			} 
		}

		public string Tax
		{
			get
			{
				//TODO - Replace with calculation
				return "$0.00";
			}
		}

		public string Total
		{
			get
			{
				//TODO - Replace with calculation
				return "$0.00";
			}
		}

		public bool CanAddToChart {
			get
			{
				bool output = false;

				//Make sure something is selected

				return output;
			}
		}

		public void AddtoChart()
		{

		}

		public bool CanRemoveToChart
		{
			get
			{
				bool output = false;

				//Make sure something is selected

				return output;
			}
		}

		public void RemoveToChart()
		{

		}

		public void CheckOut()
		{

		}
	}
}
