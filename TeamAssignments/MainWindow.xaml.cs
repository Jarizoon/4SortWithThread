using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
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

namespace TeamAssignments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> randomList = new List<int>();
        public void AddImplements()
        {
            Random rd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                int a = rd.Next(0,10000);
                randomList.Add(a);
            }
        }

        public string ConvertList(List<int> list)
        {
            string conver = "";
            foreach (var item in list)
            {               
                conver += item.ToString();
                conver += " ";
            }
            return conver;
        }
       
        public CustomData SelectionSort(List<int> randomList)
        {
            List<int> list = randomList.ToList();
            CustomData data = new CustomData();
            if(list.Count != 0) {               
                for (int i = 0; i < list.Count() - 1; i++)
                {
                    int min_index = i; 
                    for (int j = i + 1; j < list.Count() ; j++)
                    {
                        if (list[j] < list[min_index])                      
                            min_index= j;
                        }
                        if(min_index != i)
                        {
                            int temp = list[min_index];
                            list[min_index] = list[i];
                            list[i] = temp;
                        }                  
                }
            }
            data.SortList = ConvertList(list);
            data.SortThread = Thread.CurrentThread.ManagedThreadId.ToString();
            return data;
        }

        public CustomData BubbleSort(List<int> randomList)
        {
            List<int> list = randomList.ToList();
            CustomData data = new CustomData();
            int temp;
            if (list.Count != 0)
            {
                for (int j = 0; j <= list.Count - 2; j++)
                {
                    for (int i = 0; i <= list.Count - 2; i++)
                    {
                        if (list[i] > list[i + 1])
                        {
                            temp = list[i + 1];
                            list[i + 1] = list[i];
                            list[i] = temp;
                        }
                    }
                }
            }
            data.SortList = ConvertList(list);
            data.SortThread = (Thread.CurrentThread.ManagedThreadId.ToString());
            return data;
        }

        public CustomData InsertionSort(List<int> randomList)
        {
            List<int> list = randomList.ToList();
            CustomData data = new CustomData();
            if (list.Count != 0)
            {
                int i, j, key;
                for (i = 1; i < list.Count(); i++)
                {
                    key = list[i];
                    j = i - 1;
                    while(j >= 0 && list[j] > key)
                    {
                        list[j + 1] = list[j];
                        j = j - 1;
                    }
                    list[j + 1] = key;

                }
            }
            data.SortList = ConvertList(list);
            data.SortThread = Thread.CurrentThread.ManagedThreadId.ToString();
            return data;
        }


        class MergeS
        {
            void merge(List<int> list, int l, int m, int r)
            {
                int n1 = m - l + 1;
                int n2 = r - m;

                int[] L = new int[n1];
                int[] R = new int[n2];
                int i, j;

                for (i = 0; i < n1; ++i)
                    L[i] = list[l + i];
                for (j = 0; j < n2; ++j)
                    R[j] = list[m + 1 + j];

                i = 0;
                j = 0;

                int k = l;
                while (i < n1 && j < n2)
                {
                    if (L[i] <= R[j])
                    {
                        list[k] = L[i];
                        i++;
                    }
                    else
                    {
                        list[k] = R[j];
                        j++;
                    }
                    k++;
                }

                while (i < n1)
                {
                    list[k] = L[i];
                    i++;
                    k++;
                }

                while (j < n2)
                {
                    list[k] = R[j];
                    j++;
                    k++;
                }
            }

            void sort(List<int> list, int l, int r)
            {
                if (l < r)
                {
                    int m = l + (r - l) / 2;
                    sort(list, l, m);
                    sort(list, m + 1, r);
                    merge(list, l, m, r);
                }
            }

            public CustomData MergeSort(List<int> randomList)
            {
                List<int> list = randomList.ToList();
                CustomData data = new CustomData();              
                MergeS mgs = new MergeS();
                mgs.sort(list, 0, list.Count - 1);
                string conver = "";
                foreach (var item in list)
                {
                    conver += item.ToString();
                    conver += " ";
                }
                data.SortList = conver;
                data.SortThread = Thread.CurrentThread.ManagedThreadId.ToString();              
                return data;
            }
        }




        public MainWindow()
        {
            InitializeComponent();
            AddImplements();
            Selection.Text = ConvertList(randomList);
            Bubble.Text = ConvertList(randomList);
            Insertion.Text = ConvertList(randomList);
            Merge.Text = ConvertList(randomList);
        }


        private void Sync(object sender, RoutedEventArgs e)
        {   
            Stopwatch stopwatchtotal = new Stopwatch();
            stopwatchtotal.Start();    
            
            Stopwatch stopwatch1= new Stopwatch();
            stopwatch1.Start();            
            CustomData data = new CustomData();
            data = SelectionSort(randomList);
            Selection.Text = data.SortList;
            Thread1.Text = data.SortThread;
            stopwatch1.Stop();
            SelTime.Text = stopwatch1.ElapsedMilliseconds.ToString();

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            data = BubbleSort(randomList);
            Bubble.Text = data.SortList;
            Thread2.Text = data.SortThread;
            stopwatch2.Stop();
            BubTime.Text = stopwatch2.ElapsedMilliseconds.ToString();

            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            data = InsertionSort(randomList);
            Insertion.Text = data.SortList;
            Thread3.Text = data.SortThread;
            stopwatch3.Stop();
            InsTime.Text = stopwatch3.ElapsedMilliseconds.ToString();

            Stopwatch stopwatch4 = new Stopwatch();
            stopwatch4.Start();
            MergeS mgs = new MergeS();
            data = mgs.MergeSort(randomList);
            Merge.Text = data.SortList;
            Thread4.Text = data.SortThread;
            stopwatch4.Stop();
            MerTime.Text = stopwatch4.ElapsedMilliseconds.ToString();
            stopwatchtotal.Stop();
            Total.Text = stopwatchtotal.ElapsedMilliseconds.ToString();

        }

        public class CustomData
        {
            public string SortList;
            public string SortTime;
            public string SortThread;
        }
        private void ParallelSync(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatchtotal = new Stopwatch();
            stopwatchtotal.Start();
            Task task1 = Task.Factory.StartNew((Object obj) =>
            {
                CustomData customData = new CustomData();
                Stopwatch stopwatch1 = new Stopwatch();
                stopwatch1.Start();
                customData = SelectionSort(randomList);
                stopwatch1.Stop();
                CustomData data = obj as CustomData;
                data.SortThread = customData.SortThread;
                data.SortList = customData.SortList;
                data.SortTime = stopwatch1.ElapsedMilliseconds.ToString();
            }, new CustomData()
            );

            Task task2 = Task.Factory.StartNew((Object obj) =>
            {
                CustomData customData = new CustomData();
                Stopwatch stopwatch2 = new Stopwatch();
                stopwatch2.Start();
                customData = BubbleSort(randomList);
                stopwatch2.Stop();
                CustomData data = obj as CustomData;
                data.SortThread = customData.SortThread;
                data.SortList = customData.SortList;
                data.SortTime = stopwatch2.ElapsedMilliseconds.ToString();
            }, new CustomData()
            );

            Task task3 = Task.Factory.StartNew((Object obj) =>
            {
                CustomData customData = new CustomData();
                Stopwatch stopwatch3 = new Stopwatch();
                stopwatch3.Start();
                customData = InsertionSort(randomList);
                stopwatch3.Stop();
                CustomData data = obj as CustomData;
                data.SortThread = customData.SortThread;
                data.SortList = customData.SortList;
                data.SortTime = stopwatch3.ElapsedMilliseconds.ToString();
            }, new CustomData()
            );

            Task task4 = Task.Factory.StartNew((Object obj) =>
            {
                CustomData customData = new CustomData();
                Stopwatch stopwatch4 = new Stopwatch();
                stopwatch4.Start();
                MergeS mgs = new MergeS();
                customData = mgs.MergeSort(randomList);
                stopwatch4.Stop();
                CustomData data = obj as CustomData;
                data.SortThread = customData.SortThread;
                data.SortList = customData.SortList;
                data.SortTime = stopwatch4.ElapsedMilliseconds.ToString();
            }, new CustomData()
            );
            Task.WaitAll(task1, task2, task3, task4);
            var data1 = task1.AsyncState as CustomData;
            Selection.Text = data1.SortList;
            SelTime.Text = data1.SortTime;
            Thread1.Text = data1.SortThread;
            var data2 = task2.AsyncState as CustomData;
            Bubble.Text = data2.SortList;
            BubTime.Text = data2.SortTime;
            Thread2.Text = data2.SortThread;
            var data3 = task3.AsyncState as CustomData;
            Insertion.Text = data3.SortList;
            InsTime.Text = data3.SortTime;
            Thread3.Text = data3.SortThread;
            var data4 = task4.AsyncState as CustomData;
            Merge.Text = data4.SortList;
            MerTime.Text = data4.SortTime;
            Thread4.Text = data4.SortThread;
            stopwatchtotal.Stop();
            Total.Text = stopwatchtotal.ElapsedMilliseconds.ToString();
        }

        private void Async(object sender, RoutedEventArgs e)
        {
            
        }


        private void ParallelAsync(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
