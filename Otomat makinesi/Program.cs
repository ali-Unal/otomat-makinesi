namespace Otomat_makinesi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] urunler = { "Fanta", "Kola", "Su" };

            int[] fiyatlar = { 40, 40, 10 };

            int[] sira_numaraları = { 1, 2, 3 };

            int gun_sonu = 0;

            int j = sira_numaraları.Length;

            while (true)
            {

                Console.Clear();

                for (int i = 0; i < urunler.Length; i++)
                {
                    while (sira_numaraları[i] != 0)
                    {
                        Console.WriteLine($"{sira_numaraları[i]} - {urunler[i]} {fiyatlar[i]} TL");
                        break;
                    }
                }

                Console.Write("\nSeçmek istediğiniz ürünün numarasını girin: (Admin panel: - 100)");

                if (int.TryParse(Console.ReadLine(), out int menu_secim) == false)
                {
                    Console.WriteLine("Geçersiz giriş. Tekrar deneyin.");
                    Thread.Sleep(2000);
                    continue;
                }

                if (menu_secim == -100)
                {
                    AdminPanel(ref urunler, ref fiyatlar, ref sira_numaraları, ref gun_sonu, ref j);
                    continue;
                }

                int urun_secim = Array.IndexOf(sira_numaraları, menu_secim);

                if (urun_secim == -1)
                {
                    Console.WriteLine("Geçersiz ürün numarası. Tekrar deneyin.");
                    Thread.Sleep(2000);
                    continue;
                }

                Console.WriteLine($"{urunler[urun_secim]}, fiyat: {fiyatlar[urun_secim]} TL");
                Console.WriteLine("\nPara girişi yapın:");

                int para_girisi = 0;

                while (true)
                {
                    if ((int.TryParse(Console.ReadLine(), out int para_odenen) == false) || para_odenen <= 0)
                    {
                        Console.WriteLine("Geçersiz giriş, lütfen tekrar deneyin:");
                        continue;
                    }

                    para_girisi += para_odenen;

                    if (para_girisi == fiyatlar[urun_secim])
                    {
                        Console.WriteLine("Afiyet olsun");
                        gun_sonu += fiyatlar[urun_secim];
                        break;
                    }
                    else if (para_girisi > fiyatlar[urun_secim])
                    {
                        int para_ustu = para_girisi - fiyatlar[urun_secim];
                        Console.WriteLine("Afiyet olsun, Para üstünüz: " + para_ustu + " Alınız");
                        gun_sonu += fiyatlar[urun_secim];
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Yetersiz tutar. Kalan miktar: {fiyatlar[menu_secim - 1] - para_girisi} TL");
                        Console.WriteLine("\nPara eklemek için 1, para iade için 2'yi tuşlayın");

                        string yeteriz_bakiye_secim = Console.ReadLine();

                        if (yeteriz_bakiye_secim == "1")
                        {
                            Console.WriteLine("Ekleyeceğiniz miktarı girin:");
                            continue;
                        }
                        else if (yeteriz_bakiye_secim == "2")
                        {
                            Console.WriteLine(para_girisi + " TL iade edidldi");
                            para_girisi = 0;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz tuşlama, " + para_girisi + " TL iade edildi");
                            para_girisi = 0;
                            break;
                        }
                    }
                }

                Console.WriteLine("\nDevam etmek için enter'a basın");
                Console.ReadLine();
            }
        }

        static void AdminPanel(ref string[] urunler, ref int[] fiyatlar, ref int[] sira_numaraları, ref int gun_sonu, ref int j)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** Admin Panel ***\n");
                Console.WriteLine("1 - Yeni Ürün Ekle");
                Console.WriteLine("2 - Ürün Güncelle");
                Console.WriteLine("3 - Ürün Sil");
                Console.WriteLine("4 - Ürünleri Listele");
                Console.WriteLine("5 - Günsonu Toplam Satış");
                Console.WriteLine("\nAdmin panelden çıkış için 0'a basın.\n");

                string admin_panel_secim = Console.ReadLine();

                if (admin_panel_secim == "1")
                {
                    Console.WriteLine("Yeni ürünün adı:");
                    string admin_yeni_urun = Console.ReadLine();

                    while (true)
                    {
                        Console.WriteLine("Yeni ürünün fiyatı:");

                        if (int.TryParse(Console.ReadLine(), out int admin_yeni_fiyat) && admin_yeni_fiyat > 0)
                        {
                            Array.Resize(ref sira_numaraları, sira_numaraları.Length + 1);
                            Array.Resize(ref urunler, urunler.Length + 1);
                            Array.Resize(ref fiyatlar, fiyatlar.Length + 1);
                            j++;

                            sira_numaraları[sira_numaraları.Length - 1] = j;
                            urunler[urunler.Length - 1] = admin_yeni_urun;
                            fiyatlar[fiyatlar.Length - 1] = admin_yeni_fiyat;

                            Console.WriteLine("Ürün başarıyla eklendi.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz fiyat, tekrar deneyin");
                        }
                    }
                }
                else if (admin_panel_secim == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("Güncellenecek ürün numarasını girin:");

                        if (int.TryParse(Console.ReadLine(), out int admin_guncel) && admin_guncel > 0 && admin_guncel <= urunler.Length)
                        {
                            Console.WriteLine(urunler[admin_guncel - 1] + "\n");
                            Console.Write("Ürünün yeni adı: ");
                            urunler[admin_guncel - 1] = Console.ReadLine();

                            Console.Write("\nYeni fiyat:");
                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out int updatedPrice) && updatedPrice > 0)
                                {
                                    fiyatlar[admin_guncel - 1] = updatedPrice;
                                    Console.WriteLine("Ürün başarıyla güncellendi.");
                                    break;
                                }
                                else
                                {
                                    Console.Write("Geçersiz fiyat. Tekrar deneyin: ");
                                }
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz ürün numarası.");
                        }
                    }
                }
                else if (admin_panel_secim == "3")
                {
                    while (true)
                    {
                        Console.Write("Silinecek ürünün numarasını girin: ");

                        if (int.TryParse(Console.ReadLine(), out int admin_silinen) && admin_silinen > 0 && admin_silinen <= urunler.Length)
                        {
                            urunler[admin_silinen - 1] = "";
                            fiyatlar[admin_silinen - 1] = 0;
                            sira_numaraları[admin_silinen - 1] = 0;
                            Console.WriteLine("Ürün başarıyla silindi.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz ürün numarası. Tekrar deneyin");
                        }
                    }
                }
                else if (admin_panel_secim == "4")
                {
                    for (int i = 0; i < urunler.Length; i++)
                    {
                        while (sira_numaraları[i] != 0)
                        {
                            Console.WriteLine($"{sira_numaraları[i]} - {urunler[i]} {fiyatlar[i]} TL");
                            break;
                        }
                    }
                }
                else if (admin_panel_secim == "5")
                {
                    Console.WriteLine($"Günsonu Toplam Satış: {gun_sonu} TL");
                }
                else if (admin_panel_secim == "0")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Hatalı tuşlama. Tekrar deneyin. Devam etmek için enter'a basın.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("\nDevam etmek için enter'a basın");
                Console.ReadLine();
            }
        }
    }
}
