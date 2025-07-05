# 🧠 Makale Platformu Projesi - Katmanlı Mimari ve Akıllı Yorum Sistemi 🚀

.NET 8 ile geliştirilen bu projede, modern web uygulamalarında ihtiyaç duyulan **güvenlik**, **kullanıcı deneyimi** ve **ölçeklenebilirlik** göz önünde bulundurulmuştur. Platform, kullanıcıların makale yazabileceği, kategorilere göre filtreleyebileceği, yorum yapabileceği ve toksik içeriklere karşı korunabileceği bir yapıya sahiptir.

---

## 🧱 Kullanılan Katmanlı Mimari Yapı 🏗️

Projede temiz, sürdürülebilir ve okunabilir bir yapı için **katmanlı mimari** uygulanmıştır:

### 1. 🎨 Presentation (Sunum) Katmanı
- Kullanıcı arayüzü burada yer alır (Views, Controllers).
- Kullanıcıdan gelen talepler alınır ve sonuçlar görüntülenir.

### 2. 📦 Entity Katmanı
- Veritabanı modelleri burada bulunur. Örnek: `AppUser`, `Article`, `Category`, `Tag`.
- Veri yapıları tanımlanır, iş mantığı içermez.

### 3. 💾 DataAccess Katmanı
- Entity Framework Core kullanılarak veritabanı işlemleri burada gerçekleştirilir.
- CRUD işlemleri ve özel sorgular bu katmanda yer alır.

### 4. ⚙️ Business Katmanı
- Tüm iş kuralları ve mantık burada uygulanır.
- Servisler ve yöneticiler burada yer alır.

---

## 🧰 Entity Framework Özelleştirmeleri

🔍 **Entity Framework Core**’un sağladığı metotların dışında, senaryolara özgü özel sorgular da yazılmıştır.  
Örneğin:
- Bir kullanıcıya ait makaleleri getirme
- Belirli kategoriye ait makaleleri listeleme

---

## 💬 AJAX ile Yorum İşlemleri

- 🧠 Sayfa yenilenmeden, yorum gönderimi sağlandı (AJAX).
- 🔐 Kullanıcı giriş yapmamışsa, yorum kutusu gösterilmez ve giriş yapması istenir.
- 🤖 Toksik yorumlar, gönderimden önce analiz edilir.

---

## 🛡️ Slug ile URL Güvenliği

- Makale detay sayfasına erişim sağlarken `id` yerine okunabilir `slug` kullanılır.  
Örn: `/articles/guvenli-kodlama-prensipleri`  
- Bu yöntem, hem SEO açısından avantaj sağlar hem de güvenliği artırır.

---

## 👤 Identity Kütüphanesi ile Kimlik Yönetimi

- ASP.NET Identity kullanılarak kullanıcıların:
  - Kayıt
  - Giriş
  - Profil güncelleme
  - Yetkilendirme işlemleri güvenli şekilde yapılır.

---

## 📚 Entity’ler ve İlişkiler

| Entity   | Açıklama                               |
| -------- | -------------------------------------- |
| 👤 AppUser  | Platforma kayıtlı kullanıcı bilgileri     |
| 📝 Article  | Makaleler: Başlık, içerik, görsel, tarih |
| 📂 Category | Makalelerin ait olduğu kategori          |
| 🏷️ Tag      | Makalelere ait etiket bilgisi           |

### 🔗 İlişkiler:
- Her `Article`, bir `AppUser` tarafından yazılır.
- `Category`, birçok `Article` ile ilişkilidir.

---

## 🤖 Akıllı Yorum Sistemi - ToxicBERT 💬❌

Yorum sistemine yapay zeka entegre edilmiştir:

- `ToxicBERT` modeli ile kullanıcıların yazdığı yorumlar analiz edilir.
- Türkçe yorumlar, **Helsinki-NLP/opus-mt-tr-en** modeli ile İngilizceye çevrilir ve toksik içerik kontrolü yapılır.
- 🚫 Zararlı/toksik içerikler engellenir, kullanıcı bilgilendirilir.

---

## 🗂️ Sayfa Yapısı

### 🛠️ Admin Paneli

- 📊 **Dashboard:**
  - Kategori-makale dağılım grafikleri
  - En çok blog yazan yazarlar

- 📋 **Makale Listem:**
  - Kullanıcının tüm makaleleri card yapısıyla listelenir.
  - "Makaleyi Görüntüle" butonuyla detaylara ulaşılır.

- ➕ **Yeni Makale Oluştur:**
  - Başlık, görsel URL, kategori ve içerik ile yeni makale ekleme

- 👤 **Profilim:**
  - Kullanıcı bilgilerini güncelleme
  - Güncelleme sonrası otomatik çıkış ve yeniden giriş

---

### 🖥️ UI Paneli

- 🏠 **Ana Sayfa:**
  - Tüm makaleler listelenir.
  - Detayda: Yazar bilgisi, açıklama, yorumlar ve yorum yapma alanı.
  - Giriş yapılmamışsa, yorum kutusu görünmez.

- 📚 **Kategoriler:**
  - Tüm kategoriler listelenir.
  - Her kategoriye ait makaleler filtrelenerek görüntülenir.

- 🖋️ **Yazarlar:**
  - Platformdaki tüm yazarlar listelenir.
  - Yazarın tüm makalelerine erişim sağlanır.

- 🔐 **Giriş Yap:**
  - Kullanıcı, oturum açarak Admin Paneli’ne erişim sağlar ve yorum yapabilir.

---

## 🧩 BusinessLayer Extension Pattern

- `BusinessLayer` altında bir `Container > Extension` dosya yapısı kurularak,
- `Program.cs` dosyasındaki tüm `service registration` işlemleri bu `Extension` sınıfına taşınmıştır.
- Bu sayede, daha **temiz**, **modüler** ve **optimize edilmiş** bir `Program.cs` elde edilmiştir.

---

## 🛠️ Kullanılan Teknolojiler

- 💻 ASP.NET Core 8
- 🗃️ Entity Framework Core
- 🔐 ASP.NET Identity
- ⚛️ AJAX / jQuery
- 🌍 Helsinki-NLP
- 🤖 ToxicBERT (transformers)
- 🧠 Katmanlı Mimari
- 📊 Chart.js (veya alternatif JS chart kütüphanesi)

---
## 📸 Görseller

![Screenshot 2025-07-05 174103](https://github.com/user-attachments/assets/bd364437-ec56-4e56-9527-8b6758f9f01d)
![Screenshot 2025-07-05 174039](https://github.com/user-attachments/assets/08b6ada6-d912-4c1d-9f45-0eecae87d926)
![Screenshot 2025-07-05 174012](https://github.com/user-attachments/assets/9baa6ccb-6631-402b-8a4c-f1ae4a6dccd9)
![WhatsApp Görsel 2025-07-05 saat 17 49 24_a1a33e87](https://github.com/user-attachments/assets/be12b8c7-6d6b-493d-8f3b-f87cffc579dc)
![populer](https://github.com/user-attachments/assets/3178f654-9a3b-410e-9a9a-9ed5ed0839ec)
![yorum](https://github.com/user-attachments/assets/2c5ad23c-df2c-4d96-a451-c5464e22d7a0)
![Screenshot 2025-07-05 174730](https://github.com/user-attachments/assets/a7c35a03-f99e-4395-92e3-9cf3813d23da)
![Screenshot 2025-07-05 174648](https://github.com/user-attachments/assets/46a45ee4-a5e8-46df-aa6e-381bf0a0dd90)
![Screenshot 2025-07-05 174611](https://github.com/user-attachments/assets/611d640a-9905-4be6-bcc1-76d6af546f79)
![Screenshot 2025-07-05 174548](https://github.com/user-attachments/assets/54ff168b-35ad-4384-bcfd-7075f027214f)
![Screenshot 2025-07-05 174521](https://github.com/user-attachments/assets/190b9088-38c9-4660-8ecc-c5c4d94e5077)
![Screenshot 2025-07-05 174215](https://github.com/user-attachments/assets/3f6f1964-3ece-4f2a-b8ad-cffc3d7abe9d)
![Screenshot 2025-07-05 174125](https://github.com/user-attachments/assets/87dcb982-1c23-49a4-9f4e-6546db290fc3)
