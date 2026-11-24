using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using Pr1_1609.Domain;

namespace Pr1_1609.DataAccess
{
    public sealed class DBItem<T> where T : IEntity
    {
        public int counter = 1;
        public List<T> Items { get; set; } = new List<T>();

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        public int AddItem(T item)
        {
            item.Id = counter++;
            Items.Add(item);
            return item.Id;
        }

        public T GetItem(int id)
        {
            foreach (T item in Items)
            {
                if (item.Id == id) return item;
            }
            return default(T)!;
        }

        public bool UpdateItem(T oldItem, T newItem)
        {
            newItem.Id = oldItem.Id;
            if (Items.Remove(oldItem))
            {
                Items.Add(newItem);
                return true;
            }
            return false;
        }

        public bool DeleteItem(T item)
        {
            return Items.Remove(item);
        }

        public void SaveToJson(string filePath, bool backup = true)
        {
            string? dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            if (backup && File.Exists(filePath)) File.Copy(filePath, filePath + ".bak", true);

            string json = JsonSerializer.Serialize(Items, JsonOptions);
            File.WriteAllText(filePath, json);
        }

        public bool LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath)) return false;

            try
            {
                string json = File.ReadAllText(filePath);
                Items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (JsonException)
            {
                Console.WriteLine($"[Помилка] Файл {Path.GetFileName(filePath)} пошкоджено, дані не завантажено.");
                return false;
            }

            counter = Items.Count > 0 ? Items.Max(i => i.Id) + 1 : 1;
            return true;
        }
    }
}
