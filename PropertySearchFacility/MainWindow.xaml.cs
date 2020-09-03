using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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

// All pagination code has been extracted from the following source: https://www.codeproject.com/Articles/1092189/WPF-Pagination-for-DataGrid
namespace PropertySearchFacility
{
    public partial class MainWindow : Window
    {
        List<Property> Properties = new List<Property>();

        public MainWindow()
        {
            InitializeComponent();
            AvailabilityFromFilter.SelectedDate = DateTime.Today;
            PropertyGrid.Visibility = Visibility.Hidden;

            // Pagination Code
            cbNumberOfRecords.Items.Add("10");

            //Can add more records per page
            /*
            cbNumberOfRecords.Items.Add("20");
            cbNumberOfRecords.Items.Add("30");
            cbNumberOfRecords.Items.Add("50");
            cbNumberOfRecords.Items.Add("100");
            */

            cbNumberOfRecords.SelectedItem = 10;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            // Pagination Code
        }

        // I have assumed that DB number of sleeps is the minimum number of sleeps to book the property
        private void FilterSearch_Click(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            da.Connect();
            try
            {
                DateTime? availableFrom = AvailabilityFromFilter.SelectedDate.Value;
                DateTime availableTo = availableFrom.Value.AddDays(int.Parse(SleepsMinimumFilter.Text));
                string availableFromStr = availableFrom.Value.ToString("yyyy-MM-dd");
                string availableToStr = availableTo.ToString("yyyy-MM-dd");

                string location = LocationSearchFilter.Text;
                int nearBeach = YesToInt(NearBeachFilter.Text);
                int acceptsPets = YesToInt(AcceptsPetsFilter.Text);
                int minBeds = int.Parse(BedsMinimumFilter.Text);
                int minSleeps = int.Parse(SleepsMinimumFilter.Text);

                da.Query(location, nearBeach, acceptsPets, minSleeps, minBeds, availableFromStr, availableToStr);

                if (da.SqlReader != null)
                {
                    UpdateProperties(da);
                }
            }
            catch { }
        }

        private void UpdateProperties(DataAccess da)
        {
            Properties.Clear();
            while (da.SqlReader.Read())
            {
                Properties.Add(new Property() { 
                    PropertyName = da.SqlReader.GetString(2), 
                    PropertyLocation = da.SqlReader.GetString(8),
                    NumberBeds = da.SqlReader.GetInt32(6),
                    MinSleeps = da.SqlReader.GetInt32(5),
                    AllowsPets = IntToYes(da.SqlReader.GetInt32(4)), 
                    NearBeach = IntToYes(da.SqlReader.GetInt32(3)), 
                });
            }
            //Testing Pagination with 1000 fake records
            /*
            for (int i = 0; i < 1000; i++)
            {
                Property propertyObj = new Property();
                propertyObj.PropertyName = "Name " + i;
                propertyObj.PropertyLocation = "Location " + i;
                propertyObj.NearBeach = "Beach " + i;
                propertyObj.AllowsPets = "Pets " + i;
                propertyObj.MinSleeps = i;
                propertyObj.NumberBeds = i;
                Properties.Add(propertyObj);
            }
            */
            PropertyGrid.Visibility = Visibility.Visible;
            PropertyGrid.ItemsSource = Properties.Take(numberOfRecPerPage);
            int count = Properties.Take(numberOfRecPerPage).Count();
            lblpageInformation.Content = count + " of " + Properties.Count;
        }
        public string IntToYes(int i)
        {
            if(i == 1)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }
        public int YesToInt(string s)
        {
            if(s == "Yes")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // Pagination Code
        int pageIndex = 1;
        private int numberOfRecPerPage;
        //To check the paging direction according to use selection.
        private enum PagingMode
        { 
            First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 
        };
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.First);
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Next);

        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Previous);

        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Last);
        }

        private void cbNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate((int)PagingMode.PageCountChange);
        }
        private void Navigate(int mode)
        {
            int count;
            switch (mode)
            {
                case (int)PagingMode.Next:
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    if (Properties.Count >= (pageIndex * numberOfRecPerPage))
                    {
                        if (Properties.Skip(pageIndex *
                        numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
                        {
                            PropertyGrid.ItemsSource = null;
                            PropertyGrid.ItemsSource = Properties.Skip((pageIndex *
                            numberOfRecPerPage) - numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) +
                            (Properties.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                        }
                        else
                        {
                            PropertyGrid.ItemsSource = null;
                            PropertyGrid.ItemsSource = Properties.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) +
                            (Properties.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                            pageIndex++;
                        }

                        lblpageInformation.Content = count + " of " + Properties.Count;
                    }

                    else
                    {
                        btnNext.IsEnabled = false;
                        btnLast.IsEnabled = false;
                    }

                    break;
                case (int)PagingMode.Previous:
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    if (pageIndex > 1)
                    {
                        pageIndex -= 1;
                        PropertyGrid.ItemsSource = null;
                        if (pageIndex == 1)
                        {
                            PropertyGrid.ItemsSource = Properties.Take(numberOfRecPerPage);
                            count = Properties.Take(numberOfRecPerPage).Count();
                            lblpageInformation.Content = count + " of " + Properties.Count;
                        }
                        else
                        {
                            PropertyGrid.ItemsSource = Properties.Skip
                            (pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = Math.Min(pageIndex * numberOfRecPerPage, Properties.Count);
                            lblpageInformation.Content = count + " of " + Properties.Count;
                        }
                    }
                    else
                    {
                        btnPrev.IsEnabled = false;
                        btnFirst.IsEnabled = false;
                    }
                    break;

                case (int)PagingMode.First:
                    pageIndex = 2;
                    Navigate((int)PagingMode.Previous);
                    break;
                case (int)PagingMode.Last:
                    pageIndex = (Properties.Count / numberOfRecPerPage);
                    Navigate((int)PagingMode.Next);
                    break;

                case (int)PagingMode.PageCountChange:
                    pageIndex = 1;
                    numberOfRecPerPage = Convert.ToInt32(cbNumberOfRecords.SelectedItem);
                    PropertyGrid.ItemsSource = null;
                    PropertyGrid.ItemsSource = Properties.Take(numberOfRecPerPage);
                    count = (Properties.Take(numberOfRecPerPage)).Count();
                    lblpageInformation.Content = count + " of " + Properties.Count;
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    break;
            }         
        }
        // Pagination Code
    }
}
