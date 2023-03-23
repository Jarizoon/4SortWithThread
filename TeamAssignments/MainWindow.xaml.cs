using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
                int a = rd.Next(0, 10000);
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






        public MainWindow()
        {
            InitializeComponent();
            AddImplements();
            Thread6.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            Selection.Text = ConvertList(randomList);
            Bubble.Text = ConvertList(randomList);
            Insertion.Text = ConvertList(randomList);
            Merge.Text = ConvertList(randomList);
        }


        private void Sync(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatchtotal = new Stopwatch();
            stopwatchtotal.Start();

            SelectionAl();
            BubbleAl();
            InsertionAl();
            MergeAl();

            Selection.Text = pSelection;
            Thread1.Text = pThSelection;
            SelTime.Text = pTSelection;
            Bubble.Text = pBubble;
            Thread2.Text = pThBubble;
            BubTime.Text = pTBubble;
            Insertion.Text = pInsertion;
            Thread3.Text = pThInsertion;
            InsTime.Text = pTInsertion;
            Merge.Text = pMerge;
            Thread4.Text = pThMerge;
            MerTime.Text = pTMerge;
            Total.Text = totalTime;

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
            
            Thread thread = new Thread(ParallelSy);
            thread.Start();
            Thread.Sleep(5000);
            Selection.Text = pSelection;
            Thread1.Text = pThSelection;
            SelTime.Text = pTSelection;
            Bubble.Text = pBubble;
            Thread2.Text = pThBubble;
            BubTime.Text = pTBubble;
            Insertion.Text = pInsertion;
            Thread3.Text = pThInsertion;
            InsTime.Text = pTInsertion;
            Merge.Text = pMerge;
            Thread4.Text = pThMerge;
            MerTime.Text = pTMerge;
            Total.Text = totalTime;
        }

        public void ParallelSy()
        {
            Stopwatch stopwatchtotal = new Stopwatch();
            stopwatchtotal.Start();
            Parallel.Invoke(
                    () => SelectionAl(),
                    () => BubbleAl(),
                    () => InsertionAl(),
                    () => MergeAl()
                );
            stopwatchtotal.Stop();
            totalTime = stopwatchtotal.ElapsedMilliseconds.ToString();
        }

        string pSelection, pBubble, pInsertion, pMerge, pThSelection, pThBubble, pThInsertion, pThMerge, pTSelection, pTBubble, pTInsertion, pTMerge, threadUI, threadAsync, totalTime;

        public void SelectionAl()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            var list = randomList.ToList();
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
            s.Stop();
            pSelection = ConvertList(list);
            pTSelection = s.ElapsedMilliseconds.ToString();
            pThSelection = Thread.CurrentThread.ManagedThreadId.ToString();
        }

        public void BubbleAl()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            var list = randomList.ToList();
            if (list.Count != 0)
            {
                for (int i = 0; i < list.Count() - 1; i++)
                {
                    int min_index = i;
                    for (int j = i + 1; j < list.Count(); j++)
                    {
                        if (list[j] < list[min_index])
                            min_index = j;
                    }
                    if (min_index != i)
                    {
                        int temp = list[min_index];
                        list[min_index] = list[i];
                        list[i] = temp;
                    }
                }
            }
            s.Stop();
            pBubble = ConvertList(list);
            pTBubble = s.ElapsedMilliseconds.ToString();
            pThBubble = Thread.CurrentThread.ManagedThreadId.ToString();
        }

        public void InsertionAl()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            var list = randomList.ToList();
            if (list.Count != 0)
            {
                int i, j, key;
                for (i = 1; i < list.Count(); i++)
                {
                    key = list[i];
                    j = i - 1;
                    while (j >= 0 && list[j] > key)
                    {
                        list[j + 1] = list[j];
                        j = j - 1;
                    }
                    list[j + 1] = key;

                }
            }
            s.Stop();
            pInsertion = ConvertList(list);
            pTInsertion = s.ElapsedMilliseconds.ToString();
            pThInsertion = Thread.CurrentThread.ManagedThreadId.ToString();
        }

        public void MergeAl()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            List<int> list = randomList.ToList();

            sort(list, 0, list.Count - 1);

            s.Stop();
            pMerge = ConvertList(list);
            pTMerge = s.ElapsedMilliseconds.ToString();
            pThMerge = Thread.CurrentThread.ManagedThreadId.ToString();
        }

        public void merge(List<int> list, int l, int m, int r)
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

        public void sort(List<int> list, int l, int r)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;
                sort(list, l, m);
                sort(list, m + 1, r);
                merge(list, l, m, r);
            }
        }

        private void Async(object sender, RoutedEventArgs e)
        {
            Asy(); 
            Thread.Sleep(5000);
            Selection.Text = pSelection;
            Thread1.Text = pThSelection;
            SelTime.Text = pTSelection;
            Bubble.Text = pBubble;
            Thread2.Text = pThBubble;
            BubTime.Text = pTBubble;
            Insertion.Text = pInsertion;
            Thread3.Text = pThInsertion;
            InsTime.Text = pTInsertion;
            Merge.Text = pMerge;
            Thread4.Text = pThMerge;
            MerTime.Text = pTMerge;
            Total.Text = totalTime;
            Thread5.Text = threadAsync;
            Total.Text = totalTime;

        }

        public async void Asy()
        {
            Stopwatch stopwatchtotal = new Stopwatch();
            stopwatchtotal.Start();
            await Task.Run(() =>
            {
                SelectionAl();
                BubbleAl();
                InsertionAl();
                MergeAl();
                threadAsync = Thread.CurrentThread.ManagedThreadId.ToString();
            });
            stopwatchtotal.Stop();
            totalTime = stopwatchtotal.ElapsedMilliseconds.ToString();
        }


        private  void ParallelAsync(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(ParallelAsy);
            thread.Start();
            Thread.Sleep(5000);
            Selection.Text = pSelection;
            Thread1.Text = pThSelection;
            SelTime.Text = pTSelection;
            Bubble.Text = pBubble;
            Thread2.Text = pThBubble;
            BubTime.Text = pTBubble;
            Insertion.Text = pInsertion;
            Thread3.Text = pThInsertion;
            InsTime.Text = pTInsertion;
            Merge.Text = pMerge;
            Thread4.Text = pThMerge;
            MerTime.Text = pTMerge;
            Total.Text = totalTime;   
            Thread5.Text = threadAsync;
        }

        public async void ParallelAsy()
        {
            Stopwatch stopwatchtotal = new Stopwatch();
            stopwatchtotal.Start();
            threadAsync = Thread.CurrentThread.ManagedThreadId.ToString();
            Task task1 = Task.Factory.StartNew(() =>
            {
                SelectionAl();
            }
            );

            Task task2 = Task.Factory.StartNew(() =>
            {
                BubbleAl();
            }
            );

            Task task3 = Task.Factory.StartNew(() =>
            {
                InsertionAl();
            }
            );

            Task task4 = Task.Factory.StartNew(() =>
            {
                MergeAl();
            }
            );
            await Task.Run(() => Task.WaitAll(task1, task2, task3, task4));
            stopwatchtotal.Stop();
            totalTime = stopwatchtotal.ElapsedMilliseconds.ToString();
        }

    }
}
