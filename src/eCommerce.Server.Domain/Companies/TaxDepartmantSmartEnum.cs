using Ardalis.SmartEnum;

namespace eCommerce.Server.Domain.Companies;

public sealed class TaxDepartmantSmartEnum : SmartEnum<TaxDepartmantSmartEnum> 
{ 
    public static readonly TaxDepartmantSmartEnum Altındağ = new TaxDepartmantSmartEnum("Altındağ", 1);
    public static readonly TaxDepartmantSmartEnum Akyurt = new TaxDepartmantSmartEnum("Akyurt", 2);
    public static readonly TaxDepartmantSmartEnum Ankara = new TaxDepartmantSmartEnum(("Ankara"), 3);
    public static readonly TaxDepartmantSmartEnum Bala = new TaxDepartmantSmartEnum(("Bala"), 4);
    public static readonly TaxDepartmantSmartEnum Beypazarı = new TaxDepartmantSmartEnum(("Beypazarı"), 5);
    public static readonly TaxDepartmantSmartEnum Çamlıdere = new TaxDepartmantSmartEnum(("Çamlıdere"), 6);
    public static readonly TaxDepartmantSmartEnum Çankaya = new TaxDepartmantSmartEnum(("Çankaya"), 7);
    public static readonly TaxDepartmantSmartEnum Çubuk = new TaxDepartmantSmartEnum(("Çubuk"), 8);
    public static readonly TaxDepartmantSmartEnum Elmadağ = new TaxDepartmantSmartEnum(("Elmadağ"), 9);
    public static readonly TaxDepartmantSmartEnum Etimesgut = new TaxDepartmantSmartEnum(("Etimesgut"), 10);
    public static readonly TaxDepartmantSmartEnum Evren = new TaxDepartmantSmartEnum(("Evren"), 11);
    public static readonly TaxDepartmantSmartEnum Gölbaşı = new TaxDepartmantSmartEnum(("Gölbaşı"), 12);
    public static readonly TaxDepartmantSmartEnum Güdül = new TaxDepartmantSmartEnum(("Güdül"), 13);
    public static readonly TaxDepartmantSmartEnum Haymana = new TaxDepartmantSmartEnum(("Haymana"), 14);
    public static readonly TaxDepartmantSmartEnum Kahramankazan = new TaxDepartmantSmartEnum(("Kahramankazan"), 15);
    public static readonly TaxDepartmantSmartEnum Kalecik = new TaxDepartmantSmartEnum(("Kalecik"), 16);
    public static readonly TaxDepartmantSmartEnum Keçiören = new TaxDepartmantSmartEnum(("Keçiören"), 17);
    public static readonly TaxDepartmantSmartEnum Kızılcahamam = new TaxDepartmantSmartEnum(("Kızılcahamam"), 18);
    public static readonly TaxDepartmantSmartEnum Mamak = new TaxDepartmantSmartEnum(("Mamak"), 19);
    public static readonly TaxDepartmantSmartEnum Nallıhan = new TaxDepartmantSmartEnum(("Nallıhan"), 20);
    public static readonly TaxDepartmantSmartEnum Polatlı = new TaxDepartmantSmartEnum(("Polatlı"), 21);
    public static readonly TaxDepartmantSmartEnum Pursaklar = new TaxDepartmantSmartEnum(("Pursaklar"), 22);
    public static readonly TaxDepartmantSmartEnum Sincan = new TaxDepartmantSmartEnum(("Sincan"), 23);
    public static readonly TaxDepartmantSmartEnum Şereflikoçhisar = new TaxDepartmantSmartEnum(("Şereflikoçhisar"), 24);
    public static readonly TaxDepartmantSmartEnum Yenimahalle = new TaxDepartmantSmartEnum(("Yenimahalle"), 25);

    public TaxDepartmantSmartEnum(string name, int value) : base(name, value)
    {
    }
}
