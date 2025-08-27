using System;
using System.Collections.Generic;
using System.Linq;

namespace KutuphaneYonetim
{
    // Kitap sınıfı (OOP)
    class Kitap
    {
        public string Ad { get; set; }
        public string Yazar { get; set; }
        public int Yil { get; set; }
        public string ISBN { get; set; }
        public bool OduncAlindiMi { get; set; }

        public Kitap(string ad, string yazar, int yil, string isbn)
        {
            Ad = ad;
            Yazar = yazar;
            Yil = yil;
            ISBN = isbn;
            OduncAlindiMi = false;
        }

        public override string ToString()
        {
            return $"{Ad} - {Yazar} ({Yil}) | ISBN: {ISBN} | {(OduncAlindiMi ? "📕 Ödünçte" : "📗 Mevcut")}";
        }
    }

    class Program
    {
        static List<Kitap> kitaplar = new List<Kitap>();

        static void Main(string[] args)
        {
            bool devam = true;
            Console.WriteLine("=== Kütüphane Yönetim Sistemi ===\n");

            while (devam)
            {
                Console.WriteLine("Menü:");
                Console.WriteLine("1 - Kitap Ekle");
                Console.WriteLine("2 - Kitapları Listele");
                Console.WriteLine("3 - Kitap Ara");
                Console.WriteLine("4 - Kitap Ödünç Ver");
                Console.WriteLine("5 - Kitap Geri Al");
                Console.WriteLine("6 - Kitap Sil");
                Console.WriteLine("7 - Çıkış\n");

                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        KitapEkle();
                        break;
                    case "2":
                        KitaplariListele();
                        break;
                    case "3":
                        KitapAra();
                        break;
                    case "4":
                        KitapOduncVer();
                        break;
                    case "5":
                        KitapGeriAl();
                        break;
                    case "6":
                        KitapSil();
                        break;
                    case "7":
                        devam = false;
                        Console.WriteLine("Programdan çıkılıyor...");
                        break;
                    default:
                        Console.WriteLine("❌ Geçersiz seçim!\n");
                        break;
                }
            }
        }

        static void KitapEkle()
        {
            Console.Write("Kitap adı: ");
            string ad = Console.ReadLine();
            Console.Write("Yazar: ");
            string yazar = Console.ReadLine();
            Console.Write("Yıl: ");
            int yil = Convert.ToInt32(Console.ReadLine());
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();

            kitaplar.Add(new Kitap(ad, yazar, yil, isbn));
            Console.WriteLine("✅ Kitap eklendi!\n");
        }

        static void KitaplariListele()
        {
            Console.WriteLine("\n--- Kütüphanedeki Kitaplar ---");
            if (kitaplar.Count == 0)
            {
                Console.WriteLine("Hiç kitap yok.\n");
            }
            else
            {
                for (int i = 0; i < kitaplar.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {kitaplar[i]}");
                }
                Console.WriteLine();
            }
        }

        static void KitapAra()
        {
            Console.Write("Aramak istediğiniz kitap veya yazar: ");
            string arama = Console.ReadLine().ToLower();

            var bulunan = kitaplar.Where(k => k.Ad.ToLower().Contains(arama) || k.Yazar.ToLower().Contains(arama)).ToList();

            if (bulunan.Count == 0)
                Console.WriteLine("❌ Hiçbir sonuç bulunamadı.\n");
            else
            {
                Console.WriteLine("\n--- Arama Sonuçları ---");
                foreach (var kitap in bulunan)
                    Console.WriteLine(kitap);
                Console.WriteLine();
            }
        }

        static void KitapOduncVer()
        {
            KitaplariListele();
            Console.Write("Ödünç vermek istediğiniz kitabın numarasını girin: ");
            int no = Convert.ToInt32(Console.ReadLine());

            if (no > 0 && no <= kitaplar.Count)
            {
                if (!kitaplar[no - 1].OduncAlindiMi)
                {
                    kitaplar[no - 1].OduncAlindiMi = true;
                    Console.WriteLine("📕 Kitap ödünç verildi!\n");
                }
                else
                {
                    Console.WriteLine("❌ Bu kitap zaten ödünç verilmiş!\n");
                }
            }
            else
            {
                Console.WriteLine("❌ Geçersiz seçim!\n");
            }
        }

        static void KitapGeriAl()
        {
            KitaplariListele();
            Console.Write("Geri almak istediğiniz kitabın numarasını girin: ");
            int no = Convert.ToInt32(Console.ReadLine());

            if (no > 0 && no <= kitaplar.Count)
            {
                if (kitaplar[no - 1].OduncAlindiMi)
                {
                    kitaplar[no - 1].OduncAlindiMi = false;
                    Console.WriteLine("📗 Kitap geri alındı!\n");
                }
                else
                {
                    Console.WriteLine("❌ Bu kitap zaten mevcut!\n");
                }
            }
            else
            {
                Console.WriteLine("❌ Geçersiz seçim!\n");
            }
        }

        static void KitapSil()
        {
            KitaplariListele();
            Console.Write("Silmek istediğiniz kitabın numarasını girin: ");
            int no = Convert.ToInt32(Console.ReadLine());

            if (no > 0 && no <= kitaplar.Count)
            {
                Console.WriteLine($"🗑️ \"{kitaplar[no - 1].Ad}\" silindi.\n");
                kitaplar.RemoveAt(no - 1);
            }
            else
            {
                Console.WriteLine("❌ Geçersiz seçim!\n");
            }
        }
    }
}
