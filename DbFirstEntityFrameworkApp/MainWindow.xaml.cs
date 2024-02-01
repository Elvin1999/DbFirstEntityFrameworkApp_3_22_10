using DbFirstEntityFrameworkApp.DBFirstModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace DbFirstEntityFrameworkApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            //Library2Entities1 context = new Library2Entities1();

            //var result = from a in context.Authors.Include(nameof(Author.Books))
            //             select a;
            //var list=result.ToList();
            //myDataGrid.ItemsSource = list;

            //CallAdd();

            //UpdateAsync();

            CallDelete();
         //   CallGet();
            this.DataContext = this;

        }

        private async void CallGet()
        {
            await GetAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<Book> allBooks;

        public ObservableCollection<Book> AllBooks
        {
            get { return allBooks; }
            set { allBooks = value; OnPropertyChanged(); }
        }


        private async Task GetAsync()
        {
            using (var context = new Library2Entities1())
            {
                var books = await context.Books
                    .Include(nameof(Book.Author))
                    .Include(nameof(Book.Category))
                    .ToListAsync();

                AllBooks = new ObservableCollection<Book>(books);
            }
        }

        private async void CallAdd()
        {
            var count=await AddAsync();
            MessageBox.Show(count);
        }

        private async void CallDelete()
        {
            await DeleteAsync();
        }

        private async Task<string> AddAsync()
        {
            using (var context=new Library2Entities1())
            {
                var book = new Book
                {
                    Name="My New Book",
                    AuthorId=1,
                    CategoryId=1,
                    Pages=1111
                };


                //context.Books.Add(book);
                //await context.SaveChangesAsync();

                context.Entry(book).State= EntityState.Added;
                return (await context.SaveChangesAsync()).ToString();
            }
        }


        private async void UpdateAsync()
        {
            using (var context=new Library2Entities1())
            {
                var book = await context
                    .Books
                    .FirstOrDefaultAsync(b => b.Id == 5);

                if (book != null)
                {
                    book.Pages = 9999;
                    context.Entry(book).State= EntityState.Modified;    
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task DeleteAsync()
        {
            using (var context=new Library2Entities1())
            {
                var book = await context.Books
                    .FirstOrDefaultAsync(b => b.Id == 4);

                if (book != null)
                {
                    //context.Books.Remove(book);
                    //await context.SaveChangesAsync();


                    context.Entry(book).State = EntityState.Deleted;
                    await context.SaveChangesAsync();
                    await GetAsync();
                }
            }
        }

    }
}
