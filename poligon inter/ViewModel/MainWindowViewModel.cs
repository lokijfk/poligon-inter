using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using poligon_inter.View;
using System.Diagnostics;
using poligon_inter.Model;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace poligon_inter.ViewModel
{
    
    public partial class MainWindowViewModel: ObservableObject
    {


        public ObservableCollection<TreeModel> Tree { get; set; } = new();

        public MainWindowViewModel()
        {

            //ShowDialogCommand = new RelayCommand(OnShowDialog);
            //Tree = new ObservableCollection<TreeModel>();
            Tree.Add(new TreeModel { Name = "A" });
            Tree.Add(new TreeModel { Name = "B" });
            Tree[0].Children = new();
            Tree[0].Children.Add( new TreeModel { Name = "X" });
            Tree[0].Children.Add(new TreeModel { Name = "Y" });
            Tree[0].Children.Add(new TreeModel { Name = "Z" });
            Tree[1].Children = new();
            Tree[1].Children.Add(new TreeModel { Name = "X" });
            Tree[1].Children.Add(new TreeModel { Name = "Y" });
            Tree[1].Children.Add(new TreeModel { Name = "Z" });
            //Tree[1].Children[1] = new();
            //Tree[1].Children[1].Children.Add(new TreeModel { Name = "Z" });
        }

        [ObservableProperty]
        private bool _IsDialogOpen;


        /*private void ShowDialog()
        {
            OnShowDialog();
        }*/
        //public ICommand ShowDialogCommand { get; }

        [RelayCommand]
        private async Task CreateDB()
        {
             
            string x = await ShowDialog(new SimpledialogViewModel {
                Name = string.Empty, WindowName = "Utwórz nową bazę danych", Hint = "Nazwa bazy" });
            if (x != string.Empty)
            {
                Debug.WriteLine("nazwa jest : " +x);
                //tu robimy bazę danych, sprawdzić wcześniej czy nie ma juz takiej
                string path = Tools.GetUserAppDataPath();
                path = path + "\\" + x + ".db";
                if (File.Exists(path))
                {
                    Debug.WriteLine("baza o takiej nazwie już istnieje, bazy nie utworzono");
                }
                else
                {

                }
            }

           // Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + x);
        }

         
        private async Task<string> ShowDialog(SimpledialogViewModel DC)
        {
            //tak wszystko działa, pozostaje pododawać metody żeby mozna było wywołać jedno okno do różnych celów
            // DC = new SimpledialogViewModel { Name = string.Empty, WindowName = "Podaj nazwę nowej bazy", Hint = "Nazwa bazy" };
            object? view = new SimpleDialog
            {
                DataContext = DC
            };

            object? result = await DialogHost.Show(view,"RootDialog",null,null, ClosedEventHandler);

            //Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL")+" - " + DC.Name);
            
            if ((bool)result)
            {
                return DC.Name;
            }
                return "";        
        }
        private void ClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
        {
            //to jest opcjonalne, na razie zostawiam może jeszcze wykozystam
            Debug.WriteLine("You can intercept the closed event here (1)." + eventArgs.Parameter );
        }
                


    }


}
