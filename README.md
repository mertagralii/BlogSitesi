# Sale Management System

BlogSitesi, Acunmedya Akademi 11. Dönem Genişletilmiş Yazılım Uzmanlığı Eğitimi kapsamında temelden uzmanlığa geçiş sürecinde geliştirdiğim blog uygulamasıdır. Bu projede, kullanıcılar blog gönderilerini görüntüleyebilir, kategorilere göre filtreleme yapabilir ve yazarlar hakkında bilgi alabilirler. Ayrıca içerikler yönetilebilir, onaylanabilir ve silinebilir. Kullanıcılar yeni kategoriler, yeni yazılar ve yeni yazarlar ekleyebilir, kendi blog yazılarını yazabilirler

## 🚀 Özellikler
📜 Blog Yazıları: Blog yazılarının oluşturulması, güncellenmesi ve silinmesi işlemleri.
📋 Kategori Yönetimi: Kategoriler oluşturulabilir ve her blog yazısı bir kategoriye atanabilir.
✍️ Yazarlar: Blog yazıları yazarlarla ilişkilendirilebilir.
✅ Onay ve Silme: Admin kullanıcıları yazıları onaylayabilir ve silebilir.
🔗 Slugify: Başlıklardan SEO dostu URL slug'ları oluşturmak için Slugify.Core kullanıldı.

## 🛠 Kullanılan Teknolojiler
ASP.NET Core MVC: Web uygulaması için ana framework.

Microsoft SQL Server (MSSQL): Veritabanı yönetimi için kullanıldı.

Bootstrap: Responsive ve modern arayüz tasarımı için Bootstrap kullanıldı.

## 📦 Kullandığım NuGet Paketleri
Dapper: Veritabanı sorguları için hızlı ve hafif bir ORM.

Microsoft.Data.SqlClient: MSSQL ile bağlantı için gerekli SQL Client.

Slugify.Core: URL dostu slug'lar oluşturmak için kullanılan bir kütüphane.

## 📌 Kurulum

1. **Projeyi klonlayın:**
   ```sh
   git clone https://github.com/mertagralii/BlogSitesi.git
   ```
2. **Bağımlılıkları yükleyin:**
   ```sh
   dotnet restore
   ```
3. **Veritabanını oluşturun ve güncelleyin:**
   - `appsettings.json` dosyasındaki **ConnectionString** değerini kendi veritabanınıza göre güncelleyin.
   - Aşağıdaki SQL script'ini kullanarak gerekli tabloları oluşturabilirsiniz:

   ```sql
   USE [DBBlogSitesi]
    GO
    /****** Object:  Table [dbo].[TBLAuthors]    Script Date: 13.02.2025 20:36:03 ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
    CREATE TABLE [dbo].[TBLAuthors](
    	[Id] [int] IDENTITY(1,1) NOT NULL,
    	[Name] [varchar](50) NOT NULL,
    	[SurName] [varchar](50) NOT NULL,
    	[Age] [nchar](10) NOT NULL,
    	[Birthday] [datetime] NOT NULL,
    	[Birthplace] [varchar](50) NOT NULL,
    	[ImageURL] [varchar](500) NOT NULL,
    	[CreatedDate] [datetime] NOT NULL,
    	[Description] [varchar](400) NOT NULL,
    	[Summary] [varchar](100) NULL,
    	[UpdateDate] [datetime] NULL,
     CONSTRAINT [PK_TBLAuthors] PRIMARY KEY CLUSTERED 
    (
    	[Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
    /****** Object:  Table [dbo].[TBLBlog]    Script Date: 13.02.2025 20:36:03 ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
    CREATE TABLE [dbo].[TBLBlog](
    	[Id] [int] IDENTITY(1,1) NOT NULL,
    	[CategoryId] [int] NOT NULL,
    	[AuthorsId] [int] NOT NULL,
    	[Title] [nvarchar](50) NOT NULL,
    	[Summary] [varchar](60) NOT NULL,
    	[Description] [varchar](300) NOT NULL,
    	[CreatedDate] [datetime] NOT NULL,
    	[UpdateDate] [datetime] NULL,
    	[IsDeleted] [bit] NULL,
    	[IsApproved] [bit] NULL,
    	[IsIndex] [bit] NULL,
     CONSTRAINT [PK_TBLBlog] PRIMARY KEY CLUSTERED 
    (
    	[Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
    /****** Object:  Table [dbo].[TBLCategory]    Script Date: 13.02.2025 20:36:03 ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
    CREATE TABLE [dbo].[TBLCategory](
    	[Id] [int] IDENTITY(1,1) NOT NULL,
    	[CategoryName] [varchar](50) NOT NULL,
     CONSTRAINT [PK_TBLCategory] PRIMARY KEY CLUSTERED 
    (
    	[Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
    SET IDENTITY_INSERT [dbo].[TBLAuthors] ON 
    GO
    INSERT [dbo].[TBLAuthors] ([Id], [Name], [SurName], [Age], [Birthday], [Birthplace], [ImageURL], [CreatedDate], [Description], [Summary], [UpdateDate]) VALUES (1, N'Mert', N'Ağralı', N'24        ', CAST(N'2000-09-15T00:00:00.000' AS DateTime), N'Antalya', N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQaWtdPNuIx_6dFTTSM35iiysjR6qd7P1Vwfw&s', CAST(N'2025-02-04T00:00:00.000' AS DateTime), N'Ana yazi', N'Özet', CAST(N'2025-02-10T07:13:45.500' AS DateTime))
    GO
    INSERT [dbo].[TBLAuthors] ([Id], [Name], [SurName], [Age], [Birthday], [Birthplace], [ImageURL], [CreatedDate], [Description], [Summary], [UpdateDate]) VALUES (2, N'Metehan', N'Demir', N'24        ', CAST(N'2001-04-04T00:00:00.000' AS DateTime), N'Antalya', N'https://cdn-icons-png.flaticon.com/512/3135/3135715.png', CAST(N'2025-03-07T00:00:00.000' AS DateTime), N'Mete yazi', N'mete özet', NULL)
    GO
    INSERT [dbo].[TBLAuthors] ([Id], [Name], [SurName], [Age], [Birthday], [Birthplace], [ImageURL], [CreatedDate], [Description], [Summary], [UpdateDate]) VALUES (3, N'İbrahim', N'Yılmaz', N'25        ', CAST(N'2000-01-05T00:00:00.000' AS DateTime), N'Konya', N'https://static.vecteezy.com/system/resources/thumbnails/001/840/612/small_2x/picture-profile-icon-male-icon-human-or-people-sign-and-symbol-free-vector.jpg', CAST(N'2003-04-05T00:00:00.000' AS DateTime), N'İbrahim Yazi', N'İbrahim Özet', NULL)
    GO
    SET IDENTITY_INSERT [dbo].[TBLAuthors] OFF
    GO
    SET IDENTITY_INSERT [dbo].[TBLBlog] ON 
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (1, 2, 1, N'ASP.NET CORE MVC', N'.NET ÖZET', N'.NET AÇIKLAMA', CAST(N'2023-02-02T00:00:00.000' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (3, 2, 1, N'C#', N'C# Özet', N'C# Açıklama', CAST(N'2022-05-05T00:00:00.000' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (4, 3, 2, N'Kali Linux', N'KaliLinux Özet', N'Kali Linux Açıklama', CAST(N'2021-07-07T00:00:00.000' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (5, 4, 3, N'Flutter', N'Flutter Özet', N'Flutter Özet', CAST(N'2020-07-07T00:00:00.000' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (8, 2, 1, N'SQL ', N'SQL', N'SQL', CAST(N'2025-02-10T02:04:29.527' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (9, 2, 1, N'TEST', N'TEST', N'TEST', CAST(N'2019-12-20T00:00:00.000' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (10, 3, 1, N'ÖRNEK', N'ÖRNEK', N'İÇERİK ÖRNEK', CAST(N'2025-02-10T06:01:25.727' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (11, 1, 1, N'Yeni örnek', N'Yeni örnek', N'Yeni örnek', CAST(N'2025-02-10T06:02:39.900' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (12, 1, 1, N'son test1', N'son test1', N'son test1', CAST(N'2025-02-10T06:04:26.110' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (13, 1, 1, N'Silincecek', N'Silincecek', N'Silincecek', CAST(N'2025-02-10T06:11:00.397' AS DateTime), NULL, 1, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (14, 1, 1, N'böyle', N'böyle', N'böyle', CAST(N'2025-02-10T06:16:58.510' AS DateTime), NULL, 1, 0, 0)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (15, 1, 1, N'aaaaaaaaaaaaa', N'aaaaaaaaaaaaa', N'aaaaaaaaaaaaa', CAST(N'2025-02-10T06:19:07.277' AS DateTime), NULL, 1, 0, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (16, 1, 1, N'ccccccccccccc', N'ccccccccccccccc', N'cccccccccccccccc', CAST(N'2025-02-10T06:24:14.053' AS DateTime), NULL, 0, 1, 1)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (17, 1, 1, N'sanırasm çözdüm', N'sanırasm çözdüm', N'sanırasm çözdüm', CAST(N'2025-02-10T06:27:52.477' AS DateTime), NULL, 1, 0, 0)
    GO
    INSERT [dbo].[TBLBlog] ([Id], [CategoryId], [AuthorsId], [Title], [Summary], [Description], [CreatedDate], [UpdateDate], [IsDeleted], [IsApproved], [IsIndex]) VALUES (18, 1, 1, N'ana', N'ana', N'ana', CAST(N'2025-02-10T06:28:08.690' AS DateTime), NULL, 0, 1, 1)
    GO
    SET IDENTITY_INSERT [dbo].[TBLBlog] OFF
    GO
    SET IDENTITY_INSERT [dbo].[TBLCategory] ON 
    GO
    INSERT [dbo].[TBLCategory] ([Id], [CategoryName]) VALUES (1, N'Front-End')
    GO
    INSERT [dbo].[TBLCategory] ([Id], [CategoryName]) VALUES (2, N'Back-End')
    GO
    INSERT [dbo].[TBLCategory] ([Id], [CategoryName]) VALUES (3, N'Siber Güvenlik')
    GO
    INSERT [dbo].[TBLCategory] ([Id], [CategoryName]) VALUES (4, N'Mobil')
    GO
    SET IDENTITY_INSERT [dbo].[TBLCategory] OFF
    GO
    ALTER TABLE [dbo].[TBLBlog] ADD  CONSTRAINT [DF_TBLBlog_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
    GO
    ALTER TABLE [dbo].[TBLBlog] ADD  CONSTRAINT [DF_TBLBlog_IsApproved]  DEFAULT ((0)) FOR [IsApproved]
    GO
    ALTER TABLE [dbo].[TBLBlog] ADD  CONSTRAINT [DF_TBLBlog_IsIndex]  DEFAULT ((0)) FOR [IsIndex]
    GO


   ```

4. **Projeyi çalıştırın:**
   ```sh
   dotnet run
   ```
---

🎯 Geliştirmelere katkıda bulunmak isterseniz pull request gönderebilirsiniz!

