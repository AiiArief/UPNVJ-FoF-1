using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LocalizationLanguage
{
    Game, English, Indonesia
}

public class LocalizationString
{
    public string[] translatedStrings;

    public LocalizationString(string english, string indonesia, string game = "")
    {
        translatedStrings = new string[] { game, english, indonesia };
    }
}

public class LocalizationManager : MonoBehaviour
{
    public static string Translate(LocalizationString localizationString)
    {
        int languageId = PlayerPrefs.GetInt(ProfileManager.PLAYERPREFS_LANGUAGEID, 1);
        return localizationString.translatedStrings[languageId];
    }

    public static readonly LocalizationString TUTORIAL_MOVE = new LocalizationString("Press to move", "Tekan untuk bergerak");
    public static readonly LocalizationString TUTORIAL_RUN = new LocalizationString("Hold + move to run", "Tahan + gerak untuk lari");
    public static readonly LocalizationString TUTORIAL_NEXT = new LocalizationString("Next", "Lanjut");
    public static readonly LocalizationString TUTORIAL_SELECT = new LocalizationString("Select", "Pilih");

    #region Void World Dialogues

    //public static readonly LocalizationString VW_ONLOAD_LOADGAME_0 = new LocalizationString(
    //    "Wakey wakey",
    //    "Cuy, bangun cuy. Bengong aja lu daritadi. Lu masih di ... kan?"
    //);
    public static readonly LocalizationString VW_ONLOAD_LOADGAME_0_1 = new LocalizationString("(Continue)", "Eh ... iya sori baru aja mau lanjutin gamenya (Lanjutkan)");
    public static readonly LocalizationString VW_ONLOAD_LOADGAME_0_2 = new LocalizationString("(New Game)", "Ah engga ah, ngablu kali lu (Mulai dari awal)");

    static readonly string vw_onload_0 = "41 2e 2e 2e 20 61 64 61 20 70 6c 61 79 65 72 3f \n" +
        "45 68 20 62 65 6e 65 72 61 6e 20 61 64 61 20 70 6c 61 79 65 72 3f 21 21 21 \n" +
        "41 41 41 41 41 41 2c 20 41 6b 68 69 72 6e 79 61 20 61 64 61 20 79 61 6e 67 20 6d 61 69 6e 69 6e 20 6a 75 67 61 20 6e 69 68 20 67 61 6d 65 20 73 65 6e 65 6e 67 20 62 61 6e 67 65 74 20 67 75 61 61 61";
    public static readonly LocalizationString VW_ONLOAD_0 = new LocalizationString(vw_onload_0, vw_onload_0, vw_onload_0);

    public static readonly LocalizationString VW_ONLOAD_0_1 = new LocalizationString("Can you speak english? (English)", "Can you speak english? (English)", "Can you speak english? (English)");
    public static readonly LocalizationString VW_ONLOAD_0_2 = new LocalizationString("Gabisa basa enggres (Bahasa Indonesia)", "Gabisa basa enggres (Bahasa Indonesia)", "Gabisa basa enggres (Bahasa Indonesia)");

    public static readonly LocalizationString VW_ONLOAD_1 = new LocalizationString(
        "1", 
        "Oh sori, gua lupa ganti bahasa nya. \n(Fyuh, untung aja bukan bahasa inggris, grammar gua payah wkwk)"
    );

    public static readonly LocalizationString VW_ONLOAD_2 = new LocalizationString(
        "2",
        "BHAHAHAHA, selamat datang di Project Synchronous Stealth! \nKenalin gua sang pembuat game ini, panggil gua \"The Developer\"!"
    );

    public static readonly LocalizationString VW_ONLOAD_3 = new LocalizationString(
        "3",
        "Cerita dari game ini yaitu lu adalah seorang mata-mata ... \nWelp, seharusnya lu seorang mata-mata sih, tapi saat ini lu cuma player."
    );

    public static readonly LocalizationString VW_ONLOAD_4 = new LocalizationString(
        "4",
        "Nah makanya tugas lu di game ini adalah mencari tau kenapa lu bukan seorang mata-mata. \nNanti lu bakalan teleport ke dunia yang gua buat dan cari tau sendiri apa yang terjadi dengan lu dan dunia ini."
    );

    public static readonly LocalizationString VW_ONLOAD_5 = new LocalizationString(
        "5",
        "Jadi ... apakah lu udah siap untuk memainkan game ini, player?"
    );
    public static readonly LocalizationString VW_ONLOAD_5_1 = new LocalizationString("(New Game)", "Oke siap (Mulai)");
    public static readonly LocalizationString VW_ONLOAD_5_2 = new LocalizationString("(Settings)", "Mau buka pengaturan dulu (Pengaturan)");
    public static readonly LocalizationString VW_ONLOAD_5_3 = new LocalizationString("(Quit)", "Ga ah, mau tidur aja (Keluar)");

    public static readonly LocalizationString VW_ONLOAD_6 = new LocalizationString(
        "6",
        "Welp mungkin lu ada pertanyaan sih, tapi sayangnya engga gua kasih opsi di dialog lu BHAHAHAHA! \nSantai, nanti juga kejawab semua pertanyaannya."
    );

    public static readonly LocalizationString VW_ONLOAD_7 = new LocalizationString(
        "7",
        "Oke, gua akan teleport player ke Level 0 dalam waktu 3 ... 2 ... 1 ..."
    );
    #endregion
}
