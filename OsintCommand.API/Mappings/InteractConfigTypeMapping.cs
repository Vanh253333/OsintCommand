using OsintCommand.API.Entities;

namespace OsintCommand.API.Mappings
{
    public class InteractConfigTypeMapping
    {
        public static readonly Dictionary<int, Type> Map = new()
        {
            { 1, typeof(HDDocThongBaoConfig) },
            { 2, typeof(HDXemStoryConfig) },
            { 3, typeof(HDXemWatchConfig) },
            { 4, typeof(HDTuongTacNewsFeedConfig) },
            { 5, typeof(HDTuongTacBanBeConfig) },
            { 6, typeof(HDTuongTacPageConfig) },
            { 7, typeof(HDTuongTacProfileConfig) },
            { 8, typeof(HDKetBanTheoTuKhoaConfig) },
            { 9, typeof(HDKetBanTepUidConfig) },
            { 10, typeof(HDKetBanGoiYConfig) },
            { 11, typeof(HDXacNhanKetBanConfig) },
            { 12, typeof(HDHuyKetBanConfig) },
            { 13, typeof(HDThamGiaNhomTuKhoaConfig) },
            { 14, typeof(HDThamGiaNhomUidConfig) },
            { 15, typeof(HDThamGiaNhomGoiYConfig) },
            { 16, typeof(HDRoiNhomConfig) },
            { 17, typeof(HDTaoNhomConfig) },
            { 18, typeof(HDDangBaiTuongConfig) },
            { 19, typeof(HDDangBaiNhomConfig) },
            { 20, typeof(HDShareBaiConfig)},
            { 21, typeof(HDSpamBaiVietConfig) },
            { 22, typeof(HDDangReelConfig) },
            { 23, typeof(HDDangStoryConfig) },
            { 24, typeof(HDDanhGiaPageConfig) },
            { 25, typeof(HDBuffLikePageConfig) },
            { 26, typeof(HDBuffFollowUIDConfig) },
            { 27, typeof(HDTuongTacBaiVietTuKhoaConfig) },
            { 28, typeof(HDTuongTacBaiVietChiDinhConfig) },
            { 29, typeof(HDTuongTacVideoConfig) },
            { 30, typeof(HDMoiBanBeLikePageConfig) },
            { 31, typeof(HDMoiBanBeVaoNhomConfig) },
            { 32, typeof(HDTuongTacReelChiDinhConfig) },
            { 33, typeof(HDDongBoDanhBaConfig) },
            { 34, typeof(HDDoiMatKhauConfig) },
            { 35, typeof(HDUpAvatarConfig) },
            { 36, typeof(HDUpCoverConfig) },
            { 37, typeof(HDXoaSdtConfig) },
            { 38, typeof(HDOnOff2FAConfig) },
            { 39, typeof(HDAddMailConfig) },
            { 40, typeof(HDDoiTenConfig) },
            { 41, typeof(HDCapNhatThongTinConfig) },
            { 42, typeof(HDDangXuatThietBiCuConfig) },
            // ...
        };
    }
}
