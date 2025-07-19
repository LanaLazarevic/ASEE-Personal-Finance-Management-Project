using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Enums
{
    public enum MccCodeEnum
    {
        [Description("Telecommunication service including local and long distance calls, credit card calls, calls through use of magneticstrip reading telephones and fax services")]
        Code4814 = 4814,
        [Description("VisaPhone")]
        Code4815 = 4815,
        [Description("Telegraph services")]
        Code4821 = 4821,
        [Description("Money Orders - Wire Transfer")]
        Code4829 = 4829,
        [Description("Cable and other pay television (previously Cable Services)")]
        Code4899 = 4899,
        [Description("Electric, Gas, Sanitary and Water Utilities")]
        Code4900 = 4900,
        [Description("Motor vehicle supplies and new parts")]
        Code5013 = 5013,
        [Description("Office and Commercial Furniture")]
        Code5021 = 5021,
        [Description("Construction Materials, Not Elsewhere Classified")]
        Code5039 = 5039,
        [Description("Office, Photographic, Photocopy, and Microfilm Equipment")]
        Code5044 = 5044,
        [Description("Computers, Computer Peripheral Equipment, Software")]
        Code5045 = 5045,
        [Description("Commercial Equipment, Not Elsewhere Classified")]
        Code5046 = 5046,
        [Description("Medical, Dental Ophthalmic, Hospital Equipment and Supplies")]
        Code5047 = 5047,
        [Description("Metal Service Centers and Offices")]
        Code5051 = 5051,
        [Description("Electrical Parts and Equipment")]
        Code5065 = 5065,
        [Description("Hardware Equipment and Supplies")]
        Code5072 = 5072,
        [Description("Plumbing and Heating Equipment and Supplies")]
        Code5074 = 5074,
        [Description("Industrial Supplies, Not Elsewhere Classified")]
        Code5085 = 5085,
        [Description("Precious Stones and Metals, Watches and Jewelry")]
        Code5094 = 5094,
        [Description("Durable Goods, Not Elsewhere Classified")]
        Code5099 = 5099,
        [Description("Stationery, Office Supplies, Printing, and Writing Paper")]
        Code5111 = 5111,
        [Description("Drugs, Drug Proprietors, and Druggists Sundries")]
        Code5122 = 5122,
        [Description("Piece Goods, Notions, and Other Dry Goods")]
        Code5131 = 5131,
        [Description("Mens Womens and Childrens Uniforms and Commercial Clothing")]
        Code5137 = 5137,
        [Description("Commercial Footwear")]
        Code5139 = 5139,
        [Description("Chemicals and Allied Products, Not Elsewhere Classified")]
        Code5169 = 5169,
        [Description("Petroleum and Petroleum Products")]
        Code5172 = 5172,
        [Description("Books, Periodicals, and Newspapers")]
        Code5192 = 5192,
        [Description("Florists Supplies, Nursery Stock and Flowers")]
        Code5193 = 5193,
        [Description("Paints, Varnishes, and Supplies")]
        Code5198 = 5198,
        [Description("Non-durable Goods, Not Elsewhere Classified")]
        Code5199 = 5199,
        [Description("Home Supply Warehouse Stores")]
        Code5200 = 5200,
        [Description("Lumber and Building Materials Stores")]
        Code5211 = 5211,
        [Description("Glass Stores; Paint and Wallpaper Stores; Wallpaper Stores")]
        Code5231 = 5231,
        [Description("Hardware Stores")]
        Code5251 = 5251,
        [Description("Nurseries  Lawn and Garden Supply Store")]
        Code5261 = 5261,
        [Description("Mobile Home Dealers")]
        Code5271 = 5271,
        [Description("Wholesale Clubs")]
        Code5300 = 5300,
        [Description("Duty Free Store")]
        Code5309 = 5309,
        [Description("Discount Stores")]
        Code5310 = 5310,
        [Description("Department Stores")]
        Code5311 = 5311,
        [Description("Variety Stores")]
        Code5331 = 5331,
        [Description("Misc. General Merchandise")]
        Code5399 = 5399,
        [Description("Grocery Stores; Supermarkets")]
        Code5411 = 5411,
        [Description("Freezer and Locker Meat Provisioners; Meat Provisioners Freezer and Locker")]
        Code5422 = 5422,
        [Description("Candy Stores; Confectionery Stores; Nut Stores")]
        Code5441 = 5441,
        [Description("Dairy Products Stores")]
        Code5451 = 5451,
        [Description("Bakeries")]
        Code5462 = 5462,
        [Description("Misc. Food Stores  Convenience Stores and Specialty Markets")]
        Code5499 = 5499,
        [Description("Car and Truck Dealers (New and Used) Sales, Service, Repairs, Parts, and Leasing")]
        Code5511 = 5511,
        [Description("Automobile and Truck Dealers (Used Only)")]
        Code5521 = 5521,
        [Description("Automobile Supply Stores")]
        Code5531 = 5531,
        [Description("Automotive Tire Stores")]
        Code5532 = 5532,
        [Description("Automotive Parts, Accessories Stores")]
        Code5533 = 5533,
        [Description("Service Stations ( with or without ancillary services)")]
        Code5541 = 5541,
        [Description("Automated Fuel Dispensers")]
        Code5542 = 5542,
        [Description("Boat Dealers")]
        Code5551 = 5551,
        [Description("Recreational and Utility Trailers, Camp Dealers")]
        Code5561 = 5561,
        [Description("Motorcycle Dealers")]
        Code5571 = 5571,
        [Description("Motor Home Dealers")]
        Code5592 = 5592,
        [Description("Snowmobile Dealers")]
        Code5598 = 5598,
        [Description("Mens and Boys Clothing and Accessories Stores")]
        Code5611 = 5611,
        [Description("Womens Ready - to - Wear Stores")]
        Code5621 = 5621,
        [Description("Womens Accessory and Specialty Shops")]
        Code5631 = 5631,
        [Description("Childrens and Infants Wear Stores")]
        Code5641 = 5641,
        [Description("Family Clothing Stores")]
        Code5651 = 5651,
        [Description("Sports Apparel, Riding Apparel Stores")]
        Code5655 = 5655,
        [Description("Shoe Stores")]
        Code5661 = 5661,
        [Description("Furriers and Fur Shops")]
        Code5681 = 5681,
        [Description("Mens and Womens Clothing Stores")]
        Code5691 = 5691,
        [Description("Tailors, Seamstress, Mending, and Alterations")]
        Code5697 = 5697,
        [Description("Wig and Toupee Stores")]
        Code5698 = 5698,
        [Description("Miscellaneous Apparel and Accessory Shops")]
        Code5699 = 5699,
        [Description("Furniture, Home Furnishings, and Equipment Stores, Except Appliances")]
        Code5712 = 5712,
        [Description("Floor Covering Stores")]
        Code5713 = 5713,
        [Description("Drapery, Window Covering and Upholstery Stores")]
        Code5714 = 5714,
        [Description("Fireplace, Fireplace Screens, and Accessories Stores")]
        Code5718 = 5718,
        [Description("Miscellaneous Home Furnishing Specialty Stores")]
        Code5719 = 5719,
        [Description("Household Appliance Stores")]
        Code5722 = 5722,
        [Description("Electronic Sales")]
        Code5732 = 5732,
        [Description("Music Stores, Musical Instruments, Piano Sheet Music")]
        Code5733 = 5733,
        [Description("Computer Software Stores")]
        Code5734 = 5734,
        [Description("Record Shops")]
        Code5735 = 5735,
        [Description("Caterers")]
        Code5811 = 5811,
        [Description("Eating places and Restaurants")]
        Code5812 = 5812,
        [Description("Drinking Places (Alcoholic Beverages), Bars, Taverns, Cocktail lounges, Nightclubs and Discotheques")]
        Code5813 = 5813,
        [Description("Fast Food Restaurants")]
        Code5814 = 5814,
        [Description("Drug Stores and Pharmacies")]
        Code5912 = 5912,
        [Description("Package Stores - Beer, Wine, and Liquor")]
        Code5921 = 5921,
        [Description("Used Merchandise and Secondhand Stores")]
        Code5931 = 5931,
        [Description("Antique Shops - Sales, Repairs, and Restoration Services")]
        Code5832 = 5832,
        [Description("Pawn Shops and Salvage Yards")]
        Code5933 = 5933,
        [Description("Wrecking and Salvage Yards")]
        Code5935 = 5935,
        [Description("Antique Reproductions")]
        Code5937 = 5937,
        [Description("Bicycle Shops - Sales and Service")]
        Code5940 = 5940,
        [Description("Sporting Goods Stores")]
        Code5941 = 5941,
        [Description("Book Stores")]
        Code5942 = 5942,
        [Description("Stationery Stores, Office and School Supply Stores")]
        Code5943 = 5943,
        [Description("Watch, Clock, Jewelry, and Silverware Stores")]
        Code5944 = 5944,
        [Description("Hobby, Toy, and Game Shops")]
        Code5945 = 5945,
        [Description("Camera and Photographic Supply Stores")]
        Code5946 = 5946,
        [Description("Card Shops, Gift, Novelty, and Souvenir Shops")]
        Code5947 = 5947,
        [Description("Leather Foods Stores")]
        Code5948 = 5948,
        [Description("Sewing, Needle, Fabric, and Price Goods Stores")]
        Code5949 = 5949,
        [Description("Glassware/Crystal Stores")]
        Code5950 = 5950,
        [Description("Direct Marketing - Insurance Service")]
        Code5960 = 5960,
        [Description("Mail Order Houses Including Catalog Order Stores, Book/Record Clubs (No longer permitted for .S. original presentments)")]
        Code5961 = 5961,
        [Description("Direct Marketing - Travel Related Arrangements Services")]
        Code5962 = 5962,
        [Description("Door - to - Door Sales")]
        Code5963 = 5963,
        [Description("Direct Marketing - Catalog Merchant")]
        Code5964 = 5964,
        [Description("Direct Marketing - Catalog and Catalog and Retail Merchant Direct Marketing - Outbound Telemarketing Merchant")]
        Code5965 = 5965,
        [Description("Direct Marketing -Inbound Teleservices Merchant")]
        Code5967 = 5967,
        [Description("Direct Marketing - Continuity/Subscription Merchant")]
        Code5968 = 5968,
        [Description("Direct Marketing - Not Elsewhere Classified")]
        Code5969 = 5969,
        [Description("Artists Supply and Craft Shops")]
        Code5970 = 5970,
        [Description("Art Dealers and Galleries")]
        Code5971 = 5971,
        [Description("Stamp and Coin Stores - Philatelic and Numismatic Supplies")]
        Code5972 = 5972,
        [Description("Religious Goods Stores")]
        Code5973 = 5973,
        [Description("Hearing Aids - Sales, Service, and Supply Stores")]
        Code5975 = 5975,
        [Description("Orthopedic Goods Prosthetic Devices")]
        Code5976 = 5976,
        [Description("Cosmetic Stores")]
        Code5977 = 5977,
        [Description("Typewriter Stores - Sales, Rental, Service")]
        Code5978 = 5978,
        [Description("Fuel - Fuel Oil, Wood, Coal, Liquefied Petroleum")]
        Code5983 = 5983,
        [Description("Florists")]
        Code5992 = 5992,
        [Description("Cigar Stores and Stands")]
        Code5993 = 5993,
        [Description("News Dealers and Newsstands")]
        Code5994 = 5994,
        [Description("Pet Shops, Pet Foods, and Supplies Stores")]
        Code5995 = 5995,
        [Description("Swimming Pools - Sales, Service, and Supplies")]
        Code5996 = 5996,
        [Description("Electric Razor Stores - Sales and Service")]
        Code5997 = 5997,
        [Description("Tent and Awning Shops")]
        Code5998 = 5998,
        [Description("Miscellaneous and Specialty Retail Stores")]
        Code5999 = 5999,
        [Description("Financial Institutions - Manual Cash Disbursements")]
        Code6010 = 6010,
        [Description("Financial Institutions - Manual Cash Disbursements")]
        Code6011 = 6011,
        [Description("Financial Institutions - Merchandise and Services")]
        Code6012 = 6012,
        [Description("Non - Financial Institutions - Foreign Currency, Money Orders (not wire transfer) and Travelers Cheques")]
        Code6051 = 6051,
        [Description("Security Brokers/Dealers")]
        Code6211 = 6211,
        [Description("Insurance Sales, Underwriting, and Premiums")]
        Code6300 = 6300,
        [Description("Insurance Premiums, (no longer valid for first presentment work)")]
        Code6381 = 6381,
        [Description("Insurance, Not Elsewhere Classified ( no longer valid for first presentment work)")]
        Code6399 = 6399,
        [Description("Lodging - Hotels, Motels, Resorts, Central Reservation Services (not elsewhere classified)")]
        Code7011 = 7011,
        [Description("Timeshares")]
        Code7012 = 7012,
        [Description("Sporting and Recreational Camps")]
        Code7032 = 7032,
        [Description("Trailer Parks and Camp Grounds")]
        Code7033 = 7033,
        [Description("Laundry, Cleaning, and Garment Services")]
        Code7210 = 7210,
        [Description("Laundry - Family and Commercial")]
        Code7211 = 7211,
        [Description("Dry Cleaners")]
        Code7216 = 7216,
        [Description("Carpet and Upholstery Cleaning")]
        Code7217 = 7217,
        [Description("Photographic Studios")]
        Code7221 = 7221,
        [Description("Barber and Beauty Shops")]
        Code7230 = 7230,
        [Description("Shop Repair Shops and Shoe Shine Parlors, and Hat Cleaning Shops")]
        Code7251 = 7251,
        [Description("Funeral Service and Crematories")]
        Code7261 = 7261,
        [Description("Dating and Escort Services")]
        Code7273 = 7273,
        [Description("Tax Preparation Service")]
        Code7276 = 7276,
        [Description("Counseling Service - Debt, Marriage, Personal")]
        Code7277 = 7277,
        [Description("Buying/Shopping Services, Clubs")]
        Code7278 = 7278,
        [Description("Clothing Rental - Costumes, Formal Wear, Uniforms")]
        Code7296 = 7296,
        [Description("Massage Parlors")]
        Code7297 = 7297,
        [Description("Health and Beauty Shops")]
        Code7298 = 7298,
        [Description("Miscellaneous Personal Services ( not elsewhere classifies)")]
        Code7299 = 7299,
        [Description("Advertising Services")]
        Code7311 = 7311,
        [Description("Consumer Credit Reporting Agencies")]
        Code7321 = 7321,
        [Description("Blueprinting and Photocopying Services")]
        Code7332 = 7332,
        [Description("Commercial Photography, Art and Graphics")]
        Code7333 = 7333,
        [Description("Quick Copy, Reproduction and Blueprinting Services")]
        Code7338 = 7338,
        [Description("Stenographic and Secretarial Support Services")]
        Code7339 = 7339,
        [Description("Disinfecting Services; Exterminating and Disinfecting Services")]
        Code7342 = 7342,
        [Description("Cleaning and Maintenance, Janitorial Services")]
        Code7349 = 7349,
        [Description("Employment Agencies, Temporary Help Services")]
        Code7361 = 7361,
        [Description("Computer Programming, Integrated Systems Design and Data Processing Services")]
        Code7372 = 7372,
        [Description("Information Retrieval Services")]
        Code7375 = 7375,
        [Description("Computer Maintenance and Repair Services, Not Elsewhere Classified")]
        Code7379 = 7379,
        [Description("Management, Consulting, and Public Relations Services")]
        Code7392 = 7392,
        [Description("Protective and Security Services - Including Armored Cars and Guard Dogs")]
        Code7393 = 7393,
        [Description("Equipment Rental and Leasing Services, Tool Rental, Furniture Rental, and Appliance Rental")]
        Code7394 = 7394,
        [Description("Photofinishing Laboratories, Photo Developing")]
        Code7395 = 7395,
        [Description("Business Services, Not Elsewhere Classified")]
        Code7399 = 7399,
        [Description("Car Rental Companies ( Not Listed Below)")]
        Code7512 = 7512,
        [Description("Truck and Utility Trailer Rentals")]
        Code7513 = 7513,
        [Description("Motor Home and Recreational Vehicle Rentals")]
        Code7519 = 7519,
        [Description("Automobile Parking Lots and Garages")]
        Code7523 = 7523,
        [Description("Automotive Body Repair Shops")]
        Code7531 = 7531,
        [Description("Tire Re - treading and Repair Shops")]
        Code7534 = 7534,
        [Description("Paint Shops - Automotive")]
        Code7535 = 7535,
        [Description("Automotive Service Shops")]
        Code7538 = 7538,
        [Description("Car Washes")]
        Code7542 = 7542,
        [Description("Towing Services")]
        Code7549 = 7549,
        [Description("Radio Repair Shops")]
        Code7622 = 7622,
        [Description("Air Conditioning and Refrigeration Repair Shops")]
        Code7623 = 7623,
        [Description("Electrical And Small Appliance Repair Shops")]
        Code7629 = 7629,
        [Description("Watch, Clock, and Jewelry Repair")]
        Code7631 = 7631,
        [Description("Furniture, Furniture Repair, and Furniture Refinishing")]
        Code7641 = 7641,
        [Description("Welding Repair")]
        Code7692 = 7692,
        [Description("Repair Shops and Related Services - Miscellaneous")]
        Code7699 = 7699,
        [Description("Motion Pictures and Video Tape Production and Distribution")]
        Code7829 = 7829,
        [Description("Motion Picture Theaters")]
        Code7832 = 7832,
        [Description("Video Tape Rental Stores")]
        Code7841 = 7841,
        [Description("Dance Halls, Studios and Schools")]
        Code7911 = 7911,
        [Description("Theatrical Producers (Except Motion Pictures), Ticket Agencies")]
        Code7922 = 7922,
        [Description("Bands. Orchestras, and Miscellaneous Entertainers (Not Elsewhere Classified)")]
        Code7929 = 7929,
        [Description("Billiard and Pool Establishments")]
        Code7932 = 7932,
        [Description("Bowling Alleys")]
        Code7933 = 7933,
        [Description("Commercial Sports, Athletic Fields, Professional Sport Clubs, and Sport Promoters")]
        Code7941 = 7941,
        [Description("Tourist Attractions and Exhibits")]
        Code7991 = 7991,
        [Description("Golf Courses - Public")]
        Code7992 = 7992,
        [Description("Video Amusement Game Supplies")]
        Code7993 = 7993,
        [Description("Video Game Arcades/Establishments")]
        Code7994 = 7994,
        [Description("Betting (including Lottery Tickets, Casino Gaming Chips, Off - track Betting and Wagers)")]
        Code7995 = 7995,
        [Description("Amusement Parks, Carnivals, Circuses, Fortune Tellers")]
        Code7996 = 7996,
        [Description("Membership Clubs (Sports, Recreation, Athletic), Country Clubs, and Private Golf Courses")]
        Code7997 = 7997,
        [Description("Aquariums, Sea - aquariums, Dolphinariums")]
        Code7998 = 7998,
        [Description("Recreation Services (Not Elsewhere Classified)")]
        Code7999 = 7999,
        [Description("Doctors and Physicians (Not Elsewhere Classified)")]
        Code8011 = 8011,
        [Description("Dentists and Orthodontists")]
        Code8021 = 8021,
        [Description("Osteopaths")]
        Code8031 = 8031,
        [Description("Chiropractors")]
        Code8041 = 8041,
        [Description("Optometrists and Ophthalmologists")]
        Code8042 = 8042,
        [Description("Opticians, Opticians Goods and Eyeglasses")]
        Code8043 = 8043,
        [Description("Opticians, Optical Goods, and Eyeglasses (no longer valid for first presentments)")]
        Code8044 = 8044,
        [Description("Podiatrists and Chiropodists")]
        Code8049 = 8049,
        [Description("Nursing and Personal Care Facilities")]
        Code8050 = 8050,
        [Description("Hospitals")]
        Code8062 = 8062,
        [Description("Medical and Dental Laboratories")]
        Code8071 = 8071,
        [Description("Medical Services and Health Practitioners (Not Elsewhere Classified)")]
        Code8099 = 8099,
        [Description("Legal Services and Attorneys")]
        Code8111 = 8111,
        [Description("Elementary and Secondary Schools")]
        Code8211 = 8211,
        [Description("Colleges, Junior Colleges, Universities, and Professional Schools")]
        Code8220 = 8220,
        [Description("Correspondence Schools")]
        Code8241 = 8241,
        [Description("Business and Secretarial Schools")]
        Code8244 = 8244,
        [Description("Vocational Schools and Trade Schools")]
        Code8249 = 8249,
        [Description("Schools and Educational Services ( Not Elsewhere Classified)")]
        Code8299 = 8299,
        [Description("Child Care Services")]
        Code8351 = 8351,
        [Description("Charitable and Social Service Organizations")]
        Code8398 = 8398,
        [Description("Civic, Fraternal, and Social Associations")]
        Code8641 = 8641,
        [Description("Political Organizations")]
        Code8651 = 8651,
        [Description("Religious Organizations")]
        Code8661 = 8661,
        [Description("Automobile Associations")]
        Code8675 = 8675,
        [Description("Membership Organizations ( Not Elsewhere Classified)")]
        Code8699 = 8699,
        [Description("Testing Laboratories ( non - medical)")]
        Code8734 = 8734,
        [Description("Architectural - Engineering and Surveying Services")]
        Code8911 = 8911,
        [Description("Accounting, Auditing, and Bookkeeping Services")]
        Code8931 = 8931,
        [Description("Professional Services ( Not Elsewhere Defined)")]
        Code8999 = 8999,
        [Description("Court Costs, including Alimony and Child Support")]
        Code9211 = 9211,
        [Description("Fines")]
        Code9222 = 9222,
        [Description("Bail and Bond Payments")]
        Code9223 = 9223,
        [Description("Tax Payments")]
        Code9311 = 9311,
        [Description("Government Services ( Not Elsewhere Classified)")]
        Code9399 = 9399,
        [Description("Postal Services - Government Only")]
        Code9402 = 9402,
        [Description("Intra - Government Transactions")]
        Code9405 = 9405,
        [Description("Automated Referral Service (For Visa Only)")]
        Code9700 = 9700,
        [Description("Visa Credential Service ( For Visa Only)")]
        Code9701 = 9701,
        [Description("GCAS Emergency Services ( For Visa Only)")]
        Code9702 = 9702,
        [Description("Intra - Company Purchases ( For Visa Only)")]
        Code9950 = 9950,
    }

    public static class MccCodeEnumExtensions
    {
        public static string GetDescription(this MccCodeEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi == null) return value.ToString();
            var attr = fi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                         .FirstOrDefault() as DescriptionAttribute;
            return attr?.Description ?? value.ToString();
        }
    }
}
