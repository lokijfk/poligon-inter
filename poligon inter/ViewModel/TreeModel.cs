using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poligon_inter.ViewModel
{
    
    public partial class TreeModel :ObservableObject
    {
        [ObservableProperty]
        private string _Name;

        [ObservableProperty]
        private ObservableCollection<TreeModel> _Children; //{ get; set; }  = new ObservableCollection<TreeModel>();

        /*
         //to tylko pomysły 
        private int id; // w bazie
        private bool key; // to jak jest klucz
        private bool klouse; // i jest odblokowane = true, to też jako expane w tym momencie
        private bool isExpand; // to jak jest rozwinięte i ma być zwijane po zablokowaniu
        */
    }
}
