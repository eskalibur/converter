using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gera
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>  Элемент на схеме </summary>
    /// 
    /// <remarks>   Толя, 21.01.2016. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class Elem {
        public string id;
        public string name;
        public int num;
        public int count;
        public string category;
        public Elem()
        {
            count = 0;
        }

        public override string ToString()
        {
            return id + ';' + name + ';' + count.ToString();
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>  Алгоритм - осуществляет работу процедуры обработки. </summary>
    ///
    /// <remarks>   Толя, 21.01.2016. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class Algorythm
    {
        ArrayList mas_elem;
        String file_in;
        StreamReader input;
        StreamWriter output;


        public void sortirovkaID(ref ArrayList list, ref ArrayList listOut, int start, int finish)
        {
            int Size = finish - start;
            int i,j;
            Elem one, two,bufer = new Elem();
            for (i = start; i <= finish; i++)         //bubble
            {
                for (j = finish; j > i; j--)
                {
                    one = (Elem)list[j - 1];
                    two = (Elem)list[j];
                    if (one.num > two.num)
                    {
                        bufer = one; list[j - 1] = list[j]; list[j] = bufer;       
                        
                    }
                }
            
            }

        }

        public void distance (ref ArrayList list,ref int j , ref Elem bufer01, ref int dist , int count) 
        {
            // Редактировать только с наличием резервной копии
            //  Осуществляет формирование строк для вывода в файл
            // Если обнаружено 2 и более подряд идущих элемента то формируется последовательность Cx...Cy , где x и y начало и конец последовательности
            Elem buf04 = new Elem();
            Elem buf05 = new Elem();
            buf04 = (Elem)list[j];
            if ((j+1)<count) // Предотвращение выхода за границы памяти
            buf05 = (Elem)list[j + 1];
            else buf05 = (Elem)list[j];
            
            if (buf04.name == buf05.name)
            {
                if (buf05.num - buf04.num == 1)
                {
                    dist++;
                    j++;
                    distance(ref list, ref j, ref bufer01, ref dist,count);
                }
                else
                {
                    if (dist > 0)
                    {
                        buf04 = (Elem)list[j - dist];
                        buf05 = (Elem)list[j];
                        if (dist > 1)
                        {
                            if (bufer01.id != buf04.id)
                            {
                                bufer01.id += "," + buf04.id + "..." + buf05.id;
                                bufer01.count += dist + 1;
                            }
                            else
                            {
                                bufer01.id += "..." + buf05.id;
                                bufer01.count += dist;
                            }
                            for (int k = j - dist; k <= j; k++) // постановка заглушек - элемент уже учтен
                            {
                                buf04.num = 95666;
                                list[k] = buf04;
                            }
                            dist = 0;
                        }
                        else
                        {
                            if (bufer01.id != buf04.id)
                            {
                                bufer01.id += "," + buf04.id + "," + buf05.id;
                                bufer01.count += dist + 1;
                            }
                            else
                            {
                                bufer01.id += "," + buf05.id;
                                bufer01.count += dist;
                            }
                            for (int k = j - dist; k <= j; k++)
                            {
                                buf04.num = 95666;
                                list[k] = buf04;
                            }
                            dist = 0;
                        }
                    }
                    else // дистанция = 0 
                    {
                        bufer01.id += "," + buf04.id;
                        bufer01.count++;
                        buf04.num = 95666;
                        list[j] = buf04;
                        dist = 0;
                    }
                }


            }
            else // name - имеют разные значения
            {
                if (dist > 0)
                {
                    buf04 = (Elem)list[j - dist];
                    buf05 = (Elem)list[j];
                    if (dist > 1)
                    {
                        if (bufer01.id != buf04.id)
                        {
                            bufer01.id += "," + buf04.id + "..." + buf05.id;
                            bufer01.count += dist + 1;
                        }
                        else
                        {
                            bufer01.id += "..." + buf05.id;
                            bufer01.count += dist;
                        }
                        for (int k = j - dist; k <= j; k++)
                        {
                            buf04.num = 95666;
                            list[k] = buf04;
                        }
                        dist = 0;
                    }
                    else
                    {
                        if (bufer01.id != buf04.id)
                        {
                            bufer01.id += "," + buf04.id + "," + buf05.id;
                            bufer01.count += dist + 1;
                        }
                        else
                        {
                            bufer01.id += "," + buf05.id;
                            bufer01.count += dist;
                        }
                        for (int k = j - dist; k <= j; k++)
                        {
                            buf04.num = 95666;
                            list[k] = buf04;
                        }
                        dist = 0;
                    }
                }
                else
                {
                    if (bufer01.id != buf04.id)
                    {
                        bufer01.id += "," + buf04.id;
                        bufer01.count++;
                    }
                    buf04.num = 95666;
                    list[j] = buf04;
                    dist = 0;
                }
            }
        
        
        }
        public void sortNum(ref ArrayList list, ref ArrayList listOut, int count)
        {
            int i,j;
            int dist = 0;
            Elem bufer01,bufer02,bufer03 = new Elem();
   
            for (i = 0; i < count; i++)
            {
                bufer01 = (Elem)list[i];
                if (bufer01.num == 95666) continue;
             
                for (j = i; j < count; j++)
                {
                  bufer02 = (Elem)list[j];
                  if (bufer02.num == 95666) break; // continue для сортировки по name

                  if (bufer01.name == bufer02.name)
                    {
                        distance(ref list, ref j, ref bufer01, ref dist , count);
                      


                   /*   bufer01.id += "," + bufer02.id;
                        bufer01.count++;
                        bufer02.num = 95666;
                        list[j] = bufer02;     Рабочая версия, без точек*/
                    }
                  else break; // Убрать строку для сортировки по name 

                }
                listOut.Add(bufer01);
               
            } 
        
        }

        public Algorythm()
        {
            file_in = "";
            mas_elem = new ArrayList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Выполняет действие алгоритма на вновь считанном элементе. </summary>
        ///
        /// <remarks>   Толя, 21.01.2016. </remarks>
        ///
        /// <param name="elem"> The element. </param>
        ///-------------------------------------------------------------------------------------------------

      
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Парсер новой строки </summary>
        ///
        /// <remarks>   Толя, 21.01.2016. </remarks>
        ///
        /// <param name="str">  The string. </param>
        ///
        /// <returns>   экземпляр класса Elem. </returns>
        ///-------------------------------------------------------------------------------------------------

        Elem ParseNewString(string str)
        {
            
            if (str == "")
            {
                throw new NotImplementedException();
            }
            Elem e = new Elem();
            int a1, a2, a3;
            a1 = str.IndexOf('\t');
            a2 = str.IndexOf('\t', a1+1);
            a3 = str.IndexOf('\t', a2+1);
            
            e.id = str.Substring(a2+1, (a3-a2-1));
            e.name = str.Substring(a3+1);
           
            for (int i = 0; i < e.id.Length; i++ )
            {
                if (Char.IsDigit(e.id, i))
                {
                    Int32.TryParse(e.id.Substring(i), out e.num);
                    SelectCat(ref e, e.id.Substring(0, i));
                    break;
                }
            }

            e.count++ ;
            return e;
        }

        
        /// <summary>
        /// Выбор категории элемента
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cat"></param>
        void SelectCat(ref Elem e, string cat)
        {
            switch (cat)
            {
                case "C":
                    e.category = Category.capacitor;
                    break;
                case "R":
                    e.category = Category.resistors;
                    break;
                case "D":
                    e.category = Category.microchips;
                    break;
                case "X":
                    e.category = Category.connectors;
                    break;
                case "BQ":
                    e.category = Category.quartz;
                    break;
                case "L":
                    e.category = Category.inductives;
                    break;
                case "VD":
                    e.category = Category.diods;
                    break;
                case "PP":
                    e.category = Category.pp;
                    break;
                case "S":
                    e.category = Category.switchers;
                    break;
                case "XP":
                    e.category = Category.xp;
                    break;
                default:
                    e.category = Category.unknown;
                    break;

            }
        }
        
        
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Чтение данных из файла </summary>
        ///
        /// <remarks>   Толя, 21.01.2016. </remarks>
        ///
        /// <param name="file"> The file. </param>
        /// <param name="list"> [out] Отсортированнные . </param>
        ///-------------------------------------------------------------------------------------------------

        void Base(out ArrayList list)
        {
            ArrayList listOut;
            list = new ArrayList();
            listOut = new ArrayList();


            Elem e;
            while (true)
            {
                string temp;
                temp = input.ReadLine();  //!< листаем

                if (temp.Contains("__") == true)
                {
                    temp = "";
                    break;
                }
            }
            int count = 0, start=0,finish;
            while(!input.EndOfStream)
            {
                try
                {
                    list.Add(ParseNewString(input.ReadLine()));
                    Elem a,b = new Elem();
                    if (count == 0) { count++; continue; }
                    a = (Elem) list[count];
                    b = (Elem) list[count-1];
                    if (a.category != b.category ) 
                        {
                          finish=count-1; 
                          sortirovkaID(ref list,ref listOut,start,finish);
                          start=count;
                        }
                    count++;
                }
                catch (NotImplementedException)
                {
                    continue;
                }
            }
              finish = count-1;
              sortirovkaID(ref list, ref listOut, start, finish);
           
            sortNum(ref list, ref listOut, count);

         WriteDataToFile(listOut,output);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Starts this object. </summary>
        ///
        /// <remarks>   Толя, 12.02.2016. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void Start()
        {
            ArrayList copy_file;
            Base(out copy_file);
            
        }
        

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Открытие файла </summary>
        ///
        /// <remarks>   Толя, 21.01.2016. </remarks>
        ///
        /// <param name="name">  Имя файла </param>
        ///
        /// <returns>   Статус завершения. </returns>
        ///-------------------------------------------------------------------------------------------------

        public Status OpenFile(string name)
        
        {
            FileInfo a = new FileInfo(name);
            file_in = name.Substring(a.DirectoryName.Length);
            var encoding = Encoding.GetEncoding(1251);
            try
            {
                input = new StreamReader(name,encoding:encoding);
            }
            catch (IOException)
            {
                return Status.failed;
            }
            catch (ArgumentException)
            {
                return Status.failed;
            }

            return Status.success;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Открывает директорию для дальнейшего сохранения файла </summary>
        ///
        /// <remarks>   Толя, 21.01.2016. </remarks>
        ///
        /// <param name="name"> Имя файла. </param>
        ///
        /// <returns>   The Status. </returns>
        ///-------------------------------------------------------------------------------------------------

        public Status OpenDirectory(string name)
        {
            if (Directory.Exists(name) == false)
            {
                return Status.failed;
            }
            
            FileStream outputFile;
            try
            {
                outputFile = File.Create(name + file_in + ".csv");  // сочиняем имя файла
                outputFile.Close();
                file_in = name + file_in + ".csv";
            }
            catch (UnauthorizedAccessException)
            {
                return Status.unauth_access;
            }
            catch (IOException)
            {
                return Status.failed;
            }

            
            output = new StreamWriter(file_in);
           // output.WriteLine("Start");
            return Status.success;
        }

        /// <summary>
        /// Запись данных из массива в файл
        /// </summary>
        /// <param name="listOut"></param>
        void WriteDataToFile(ArrayList listOut, StreamWriter output)
        {
            
            Elem buf1 = new Elem();
            Elem buf2 = new Elem();
            foreach (Elem i in listOut)
            {
                buf2 = buf1;
                buf1 = (Elem)i;
                if (buf1.category != buf2.category)
                {
                    output.WriteLine(';'+buf1.category+';');
                }
                output.WriteLine(i.ToString());
            }
            output.Flush();
            output.Close();
        }
    }
}
