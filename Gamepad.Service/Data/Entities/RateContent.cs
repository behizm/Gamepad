namespace Gamepad.Service.Data.Entities
{
    internal class RateContent : BaseEntity
    {
        public RateSource RateSource { get; set; }
        public RateContentValue Content { get; set; }
    }

    public enum RateContentValue
    {
        EsrbAlcoholReference = 0,
        EsrbAnimatedBlood = 1,
        EsrbBlood = 2,
        EsrbBloodAndGore = 3,
        EsrbCartoonViolence = 4,
        EsrbComicMischief = 5,
        EsrbCrudeHumor = 6,
        EsrbDrugReference = 7,
        EsrbFantasyViolence = 8,
        EsrbIntenseViolence = 9,
        EsrbLanguage = 10,
        EsrbLyrics = 11,
        EsrbMatureHumor = 12,
        EsrbNudity = 13,
        EsrbPartialNudity = 14,
        EsrbRealGambling = 15,
        EsrbSexualContent = 16,
        EsrbSexualThemes = 17,
        EsrbSexualViolence = 18,
        EsrbSimulatedGambling = 19,
        EsrbStrongLanguage = 20,
        EsrbStrongLyrics = 21,
        EsrbStrongSexualContent = 22,
        EsrbSuggestiveThemes = 23,
        EsrbTobaccoReference = 24,
        EsrbUseOfAlcohol = 25,
        EsrbUseOfDrugs = 26,
        EsrbUseOfTobacco = 27,
        EsrbViolence = 28,
        EsrbViolentReferences = 29,

        PegiBadLanguage = 40,
        PegiDiscrimination = 41,
        PegiDrugs = 42,
        PegiFear = 43,
        PegiGambling = 44,
        PegiSex = 45,
        PegiViolence = 46,
        PegiOnlineGameplay = 47
    }
}
