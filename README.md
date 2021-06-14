## ASP.NET Core Web API layihəsi

.Net Core framework ilə yazılmış bir cross-platform Online Ticarət proyektidir. Layihənin yaşam döngüsü boyunca SOLID prinsiplərinə uyğunlaşmasına və Clean Code şəklindı  yazılmasına xüsusi diqqət yetirilmişdir. Kodların base sistemdən idarə olunması üçün Repository və digər məqsədlər üçün çeşidli Design Pattern-lərdən istifadə edilmişdir. *Layer Architecture*  üslubunda yazılmış layihədə istifadə olunan texnologiyalar proyektdə əsla bağımlılıq yaratmırlar. Təbəqələr (layer) bir-biri ilə abstrakt (loosely coupled) şəkildə əlaqələndirilmişdir. Kodlar Test Driven Development(TDD) ilə test-first yazılmışdır.  Təhlükəsizlik üçün JWT (Json Web Token) istifadə edilmişdir. İstfiadə olunmuş bəzi texnologiyalar:
 
- ####  EntityFrameworkCore (ORM dəstəyi)
- ####  Autofac (AOP və IoC dəstəyi)
- ####  FluentValidation (Validation dəstəyi)
- ####  Moq (Unit test dəstəyi)
