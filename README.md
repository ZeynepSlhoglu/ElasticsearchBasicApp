 

# ElasticsearchBasicApp
 
Bu proje, .NET 8 Blazor frameworkü ve Elasticsearch kullanarak www.sozcu.com.tr web sitesindeki astroloji haberlerini çeken ve kullanıcıların bu verileri arayabileceği bir web uygulamasıdır.
 
## Özellikler

- **Veri Toplama:** www.sozcu.com.tr/astroloji sayfasındaki astroloji haberlerini otomatik olarak toplar.
- **Elasticsearch Entegrasyonu:** Çekilen haberler Elasticsearch'e indekslenir, bu sayede arama işlemleri yapılabilir.
- **Blazor Arayüzü:** Kullanıcı dostu bir arayüz ile haberler görüntülenebilir, aranabilir ve yenilenebilir.

## Gereksinimler

Bu projeyi çalıştırmak için aşağıdaki gereksinimlere ihtiyacınız olacak:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 
- [Docker](https://www.docker.com)

## Elasticsearch'in Docker Compose Üzerinde Çalıştırılması
Bu projede, Elasticsearch Docker Compose kullanılarak çalıştırılmaktadır. Elasticsearch'i çalıştırmak için aşağıdaki adımları izleyin:

- Terminalden projenizin kök dizinine gidin ve Elasticsearch'i başlatmak için şu komutu çalıştırın:

``` bash 
docker-compose up -d 
```
- Elasticsearch'in çalıştığından emin olmak için aşağıdaki URL'yi tarayıcınızda açın:

``` arduino 
http://localhost:9200 
```
Bu sayfa Elasticsearch'in durumunu gösterecektir.

## Kullanım
- Veri Çekme: Anasayfada "Verileri getir" butonuna tıklayarak www.sozcu.com.tr/astroloji sayfasındaki verileri çekin.

- Arama: Arama çubuğuna aramak istediğiniz kelimeleri girerek mevcut haberler arasında arama yapın.

- Verileri Temizleme ve Yeniden Çekme: "Verileri getir" butonuna tıkladığınızda, Elasticsearch'teki tüm veriler silinir ve yeni veriler çekilir.

## Mimari
Bu proje, Blazor Server-Side mimarisini kullanarak, kullanıcı arayüzünü sunar. Aşağıdaki bileşenlerden oluşur:

- CrawlService: ** www.sozcu.com.tr/astroloji sayfasından haberleri çeker.
- ElasticsearchService: Çekilen haberleri Elasticsearch'e kaydeder, temizler ve arama yapar.
- Article Modeli: Haberlerin başlık, açıklama, görsel ve URL bilgilerini tutar.
