using System;
using System.Collections.Generic;
using System.Text;

namespace Gera
{
    interface UIForm
    {
        void FileOpenStatus(Status st);
        void DirectoryOpenStatus(Status st);
        void ProcessStatus(Status st); 
    }

    public enum Status { success, failed, unauth_access};

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Категории элементов </summary>
    ///
    /// <remarks>   Толя, 21.01.2016. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public static class Category
    {
        public const string microchips = "Микросхемы";
        public const string capacitor = "Конденсаторы";
        public const string resistors = "Резисторы";
        public const string connectors = "Соединения контактные";
        public const string quartz = "Кварцевые резонаторы";
        public const string switchers = "Переключатели";
        public const string transistors = "Транзисторы";
        public const string inductives = "Катушка индуктивности";
        public const string diods = "Диоды";
        public const string unknown = "Неопознанная категория";
        public const string xp = "Категория ХР";
        public const string pp = "Категория РР";

    }
}
