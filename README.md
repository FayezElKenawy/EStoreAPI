## ASP.NET Core Web API layihəsi

.Net Core framework ilə yazılmış bir cross-platform Online Ticarət proyektidir. Layihənin yaşam döngüsü boyunca SOLID prinsiplərinə uyğunlaşmasına və Clean Code şəklindı  yazılmasına xüsusi diqqət yetirilmişdir. Kodların base sistemdən idarə olunması üçün Repository və digər məqsədlər üçün çeşidli Design Pattern-lərdən istifadə edilmişdir. *Layer Architecture*  üslubunda yazılmış layihədə istifadə olunan texnologiyalar proyektdə əsla bağımlılıq yaratmırlar. Təbəqələr (layer) bir-biri ilə abstrakt (loosely coupled) şəkildə əlaqələndirilmişdir. Kodlar Test Driven Development(TDD) ilə test-first yazılmışdır.  Təhlükəsizlik üçün JWT (Json Web Token) istifadə edilmişdir. 

 ### Təbəqələr (layers):

- ####  [Entities](https://github.com/SubhanMasimov/EStoreAPI/tree/master/Entities "Entities")
Database cədvəllərinin ORM üçün obyekt halında saxlandığı layer. Hər databaza cədvəlinin qarşılığı burada bir sinifdir.

- ####  [DataAccess](https://github.com/SubhanMasimov/EStoreAPI/tree/master/DataAccess "DataAccess")
Database ilə əlaqə qurulduğu və ORM kodlarının yazıldığı layer. Database obyektləri burada konfiqurasiya edilir.

- ####  [Business](https://github.com/SubhanMasimov/EStoreAPI/tree/master/Business "Business")
AOP tətbiq olunduğu və business logic-lərin yazıldığı layer.

- ####  [Business](https://github.com/SubhanMasimov/EStoreAPI/tree/master/Business "Business")
AOP tətbiq olunduğu və business logic-lərin yazıldığı layer.

- ####  [Tests](https://github.com/SubhanMasimov/EStoreAPI/tree/master/Tests "Tests")
Unit testlərin yazıldığı layer. **Moq** dəstəklidir.

- ####  [Core](https://github.com/SubhanMasimov/EStoreAPI/tree/master/Core "Core")
Proyekt asılılığı olmadan bütün .Net Core proyektlərində istifadə oluna bilən kodların yazıldığı layer. [Cross Cutting Concerns](https://github.com/SubhanMasimov/EStoreAPI/tree/master/Core/CrossCuttingConcerns "Cross Cutting Concerns"), [Utilities](https://github.com/SubhanMasimov/EStoreAPI/tree/master/Core/Utilities "Utilities") kimi bir sıra köməkçi alt-təbəqələr yazılmışdır.

- ####  [WebAPI](https://github.com/SubhanMasimov/EStoreAPI/tree/master/WebAPI "WebAPI")
Restful API-nin yazıldığı və Controller-lərin konfiqurasiya oldunduğu layer. Proyektin Startup təbəqəsidir. **Swagger** dəstəklidir.


İstfiadə olunmuş bəzi texnologiyalar:
 
- ####  EntityFrameworkCore (ORM dəstəyi)
- ####  Autofac (AOP və IoC dəstəyi)
- ####  FluentValidation (Validation dəstəyi)
- ####  Moq (Unit test dəstəyi)
- #### Swagger (Docs. dəstəyi)
