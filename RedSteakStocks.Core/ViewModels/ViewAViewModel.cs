﻿using Prism.Mvvm;

namespace RedSteakStocks.Core.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewAViewModel()
        {
            Message = "View A from your Prism Module";
        }
    }
}