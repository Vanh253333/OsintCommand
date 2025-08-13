using System.ComponentModel;

namespace OsintCommand.API.Entities
{
    public enum InteractType
    {
        // tương tác
        [Description("Đọc thông báo")]
        HDDocThongBao = 1,

        [Description("Tương tác story")]
        HDXemStory = 2,

        [Description("Tương tác watch")]
        HDXemWatch = 3,

        [Description("Tương tác Newsfeed")]
        HDTuongTacNewsfeed = 4,

        [Description("Tương tác bạn bè")]
        HDTuongTacBanBe = 5,

        [Description("Tương tác page")]
        HDTuongTacPage = 6,

        [Description("Tương tác profile")]
        HDTuongTacProfile = 7,


        // kết bạn - tham gia nhóm
        [Description("Kết bạn theo từ khóa")]
        HDKetBanTheoTuKhoa = 8,

        [Description("Kết bạn theo tệp UID")]
        HDKetBanTepUid = 9,

        [Description("Kết bạn gợi ý")]
        HDKetBanGoiY = 10,

        [Description("Xác nhận kết bạn")]
        HDXacNhanKetBan = 11,

        [Description("Hủy kết bạn")]
        HDHuyKetBan = 12,

        [Description("Tham gia nhóm từ khóa")]
        HDThamGiaNhomTuKhoa = 13,

        [Description("Tham gia nhóm chỉ định")]
        HDThamGiaNhomUid = 14,

        [Description("Tham gia nhóm gợi ý")]
        HDThamGiaNhomGoiY = 15,

        [Description("Rời nhóm")]
        HDRoiNhom = 16,

        [Description("Tạo nhóm")]
        HDTaoNhom = 17,


        // đăng bài - share bài - spam
        [Description("Đăng bài lên tường")]
        HDDangBaiTuong = 18,

        [Description("Đăng bài lên nhóm")]
        HDDangBaiNhom = 19,

        //[Description("")]
        //HDDangBaiPage = 20,

        [Description("Share bài nâng cao")]
        HDShareBai = 20,

        [Description("Spam bài viết")]
        HDSpamBaiViet = 21,

        [Description("Đăng reel")]
        HDDangReel = 22,

        [Description("Đăng story")]
        HDDangStory = 23,

        // seeding
        [Description("Đánh giá page")]
        HDDanhGiaPage = 24,

        [Description("Buff like page")]
        HDBuffLikePage = 25,

        [Description("Buf follow UID")]
        HDBuffFollowUID = 26,

        [Description("Tương tác bài viết theo từ khóa")]
        HDTuongTacBaiVietTuKhoa = 27,

        [Description("Tương tác bài viết theo chỉ định")]
        HDTuongTacBaiVietChiDinh = 28,

        [Description("Tương tác video")]
        HDTuongTacVideo = 29,

        //[Description("")]
        //HDTuongTacLivestream   = 31,

        [Description("Mời bạn bè like page")]
        HDMoiBanBeLikePage = 30,

        [Description("Mời bạn bè vào nhóm")]
        HDMoiBanBeVaoNhom = 31,

        [Description("Tương tác reel chỉ định")]
        HDTuongTacReelChiDinh = 32,

        [Description("Đồng bộ danh bạ")]
        HDDongBoDanhBa = 33,

        // đổi thông tin
        [Description("Đổi mật khẩu")]
        HDDoiMatKhau = 34,

        [Description("Up avatar")]
        HDUpAvatar = 35,

        [Description("Up cover")]
        HDUpCover = 36,

        [Description("Xóa số điện thoại")]
        HDXoaSdt = 37,

        [Description("Bật - tắt 2FA")]
        HDOnOff2FA  = 38,

        [Description("Thêm mail")]
        HDAddMail = 39,

        [Description("Đổi tên")]
        HDDoiTen = 40,

        [Description("Cập nhật thông tin")]
        HDCapNhatThongTin = 41,

        [Description("Đăng xuất thiết bị cũ")]
        HDDangXuatThietBiCu = 42,
    }
}
