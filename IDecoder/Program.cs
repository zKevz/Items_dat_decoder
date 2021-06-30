using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;

namespace IDecoder
{
    class Program
    {
        static byte[] Data { get; set; }
        static int Mempos { get; set; } = 0;
		const string Secret = "PBG892FXX982ABC*";
		static async Task Main()
        {
            if (!File.Exists("items.dat"))
            {
                Console.WriteLine("Items Dat not found!");
				Environment.Exit(1);
				return;
            }
            Data = await File.ReadAllBytesAsync("items.dat");
            short itemsDatVersion = ReadShort();
            Add(2);
            int itemCount = ReadInt();
            Add(4);
			JArray arr = new JArray();
			int hey = itemCount / 100, count = 0;
            for(int i = 0; i < itemCount; i++)
            {
				int itemID = ReadInt();
				Add(4);
				int editabletype = Data[Mempos];
				Mempos++;
				int itemCategory = Data[Mempos];
				Mempos++;
				int actionType = Data[Mempos];
				Mempos++;
				int hitSoundType = Data[Mempos];
				Mempos++;
                int len = Data[Mempos];
				Add(2);
				byte[] bname = new byte[len];
				for(int j = 0; j < len; j++)
                {
					bname[j] = (byte)(Data[Mempos] ^ (Secret[(j + itemID) % Secret.Length]));
					Mempos++;
				}
				string name = Encoding.UTF8.GetString(bname);
				len = Data[Mempos];
				Mempos += 2;
				byte[] textureb = new byte[len];
				for(int j = 0; j < len; j++)
                {
					textureb[j] = Data[Mempos];
					Mempos++;
                }
				string texture = Encoding.UTF8.GetString(textureb);

				int texturehash = ReadInt();
				Add(4);
				int itemkind = Data[Mempos];
				Mempos++;
				int val1 = ReadInt();
				Mempos += 4;
				int texturex = Data[Mempos];
				Mempos++;
                int texturey = Data[Mempos];
				Mempos++;
                int spreadtype = Data[Mempos];
				Mempos++;
                int isstripeywallpaper = Data[Mempos];
				Mempos++;
                int collisiontype = Data[Mempos];
				Mempos++;
				int breakhits = Data[Mempos];
				Mempos++;
				int dropchance = ReadInt();
				Mempos += 4;
				int clothingtype = Data[Mempos];
				Mempos++;
				short rarity = BitConverter.ToInt16(Data,Mempos);
				Mempos += 2;
				int maxamount = Data[Mempos];
				Mempos++;
				len = Data[Mempos];
				Mempos += 2;
				byte[] extrafileb = new byte[len];
				for(int j = 0; j < len; j++)
                {
					extrafileb[j] = Data[Mempos];
					Mempos++;
                }
				string extraFile = Encoding.UTF8.GetString(extrafileb);
				int extrafilehash = ReadInt();
				Mempos += 4;
				int audiovolume = ReadInt();
				Mempos += 4;
				len = Data[Mempos];
				Mempos += 2;
				byte[] petnameb = new byte[len];
				for (int j = 0; j < len; j++)
				{
					petnameb[j] = Data[Mempos];
					Mempos++;
				}
				string petName = Encoding.UTF8.GetString(petnameb);

				len = Data[Mempos];
                Mempos += 2;
				byte[] petprefixb = new byte[len];
				for(int j = 0; j < len; j++)
                {
					petprefixb[j] = Data[Mempos];
					Mempos++;
                }
				string petPrefix = Encoding.UTF8.GetString(petprefixb);

				len = Data[Mempos];
				Mempos += 2;
				byte[] petsuffixb = new byte[len];
				for (int j = 0; j < len; j++)
				{
					petsuffixb[j] = Data[Mempos];
					Mempos++;
				}
				string petsuffix = Encoding.UTF8.GetString(petsuffixb);

				len = Data[Mempos];
				Mempos += 2;
				byte[] petabilityb = new byte[len];
                for (int j = 0; j < len; j++)
                {
                    petabilityb[j] = Data[Mempos];
                    Mempos++;
                }
				string petability = Encoding.UTF8.GetString(petabilityb);
				int seedbase = Data[Mempos];
				Mempos++;
				int seedoverlay = Data[Mempos];
				Mempos++;
				int treebase = Data[Mempos];
				Mempos++;
				int treeleaves = Data[Mempos];
				Mempos++;
				int seedcolor = ReadInt();
				Mempos += 4;
				int seedoverlaycolor = ReadInt();
				Mempos += 4;
				Mempos += 4; // ingredient
                int growtime = ReadInt();
				Mempos += 4;
				short val2 = BitConverter.ToInt16(Data,Mempos);
				Mempos += 2;
				short israyman = BitConverter.ToInt16(Data, Mempos);
				Mempos += 2;

				len = Data[Mempos];
				Mempos += 2;
				byte[] extraoptionsb = new byte[len];
				for(int j = 0; j < len; j++)
                {
					extraoptionsb[j] = Data[Mempos];
					Mempos++;
                }
				string extraoptions = Encoding.UTF8.GetString(extraoptionsb);

				len = Data[Mempos];
				Mempos += 2;
				byte[] texture2b = new byte[len];
				for (int j = 0; j < len; j++)
				{
					texture2b[j] = Data[Mempos];
					Mempos++;
				}
				string texture2 = Encoding.UTF8.GetString(texture2b);

				len = Data[Mempos];
				Mempos += 2;
		    		byte[] extraoptions2b = new byte[len];
				for (int j = 0; j < len; j++)
				{
					extraoptions2b[j] = Data[Mempos];
					Mempos++;
				}
		    		string extraoptions2 = Encoding.UTF8.GetString(extraoptions2b);

				Mempos += 80;
				string punchOptions = "";
				if (itemsDatVersion >= 11)
                {
					len = Data[Mempos];
					byte[] punchOptionsb = new byte[len];
					Mempos += 2;
					for (int j = 0; j < len; j++)
					{
						punchOptionsb[j] = Data[Mempos];
						Mempos++;
					}
					punchOptions = Encoding.UTF8.GetString(punchOptionsb);
				}
		    if (itemsDatVersion >= 12) {
			    Mempos += 13;
		    }
		    if (itemsDatVersion >= 13) {
			    Mempos += 4;
		    }
				JObject jObj = new JObject(
					new JProperty("itemid", itemID),
					new JProperty("editabletype",editabletype),
					new JProperty("itemcategory",itemCategory),
					new JProperty("actiontype",actionType),
					new JProperty("hitsoundtype",hitSoundType),
					new JProperty("name",name),
					new JProperty("texture",texture),
					new JProperty("texturehash",texturehash),
					new JProperty("itemkind",itemkind),
					new JProperty("val1",val1),
					new JProperty("texturex",texturex),
					new JProperty("texturey",texturey),
					new JProperty("spreadtype",spreadtype),
					new JProperty("isstripeywallpaper",isstripeywallpaper),
					new JProperty("collisiontype",collisiontype),
					new JProperty("breakhits",breakhits),
					new JProperty("dropchance",dropchance),
					new JProperty("clothingtype",clothingtype),
					new JProperty("rarity",rarity),
					new JProperty("maxamount",maxamount),
					new JProperty("extrafile",extraFile),
					new JProperty("extrafilehash",extrafilehash),
					new JProperty("audiovolume",audiovolume),
					new JProperty("petname",petName),
					new JProperty("petprefix",petPrefix),
					new JProperty("petsuffix",petsuffix),
					new JProperty("petability",petability),
					new JProperty("seedbase",seedbase),
					new JProperty("seedoverlay",seedoverlay),
					new JProperty("treebase",treebase),
					new JProperty("treeleaves",treeleaves),
					new JProperty("seedcolor",seedcolor),
					new JProperty("seedoverlaycolor",seedoverlaycolor),
					new JProperty("growtime",growtime),
					new JProperty("val2",val2),
					new JProperty("israyman", israyman),
					new JProperty("extraoptions",extraoptions),
					new JProperty("texture2",texture2),
					new JProperty("extraoptions2",extraoptions2),
					new JProperty("punchoptions",punchOptions));
				arr.Add(jObj);
				if(i % hey == 0)
                {
                    Console.WriteLine("Waiting.. " + count + "%");
					count++;
				}
			}
            JObject json = new JObject(
                new JProperty("itemsDatVersion", itemsDatVersion),
                new JProperty("items", arr));
            await File.WriteAllTextAsync("data.json", json.ToString());

			Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nDone! Don't forget to credit GrowtopiaNoobs and me (kevz#1227)!");
            Console.WriteLine("The location file should be in the same folder as your .exe in.");
            Console.WriteLine("Press any key to continue..\n\n");
			Console.ResetColor();
			Console.ReadKey();
        }
        static void Add(int a) => Mempos += a;
        static int ReadInt() => BitConverter.ToInt32(Data, Mempos);
        static short ReadShort() => BitConverter.ToInt16(Data, Mempos);
    }
}
