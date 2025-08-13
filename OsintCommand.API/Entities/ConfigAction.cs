namespace OsintCommand.API.Entities
{

    public class CommonConfig
    {
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public int nudDelayFrom { get; set; }
        public int nudDelayTo { get; set; }
    }

    public class TuongTacConfig : CommonConfig
    {
        public int nudTimeFrom { get; set; }
        public int nudTimeTo { get; set; }
        public bool ckbInteract { get; set; }
        public int nudPercentLike { get; set; }
        public List<int> typeReaction { get; set; }
        public bool ckbSendAnh { get; set; }
        public int nudPercentCommentImage { get; set; }
        public string txtAnh { get; set; }
        public bool ckbShareWall { get; set; }
        public int nudPercentShareWall { get; set; }
        public List<string> txtContentShare { get; set; }
        public bool ckbComment { get; set; }
        public int nudPercentCommentText { get; set; }
        public List<string> txtComment { get; set; }
        public bool ckbSticker { get; set; } = false;
        public int typeBinhLuan { get; set; } // 0: chỉ định một trong hai, 1: ngẫu nhiên một trong 2   
    }

    public class HDDocThongBaoConfig : CommonConfig
    {
        public bool ckbXoaThongBaoSpam { get; set; } = false; // ???????????????    
    }

    public class HDXemStoryConfig : TuongTacConfig
    {
        public bool ckbTuDongXoaNoiDung { get; set; }
        public int cbbOptionsPost { get; set; } // 0: giới hạn số story tương tác, 1: giới hạn thời gian tương tác
        public int nudThoiGianFrom { get; set; }
        public int nudThoiGianTo { get; set; }

    }

    public class HDXemWatchConfig : TuongTacConfig
    {
        public bool ckbTuDongXoaNoiDung { get; set; }
        public int cbbOptionsPost { get; set; } // 0: giới hạn số video tương tác, 1: giới hạn thời gian tương tác
        public int nudThoiGianFrom { get; set; }
        public int nudThoiGianTo { get; set; }
        public int cbbDoiTuong { get; set; } // 0: tương tác ngẫu nhiên, 1: tương tác theo từ khóa
        public List<string> txtTuKhoa { get; set; }
    }

    public class HDTuongTacNewsFeedConfig : TuongTacConfig
    {
        public bool ckbTuDongXoaNoiDung { get; set; }
        public int cbbOptionsPost { get; set; } // 0: giới hạn số bài viết tương tác, 1: giới hạn thời gian tương tác
        public int nudThoiGianFrom { get; set; }
        public int nudThoiGianTo { get; set; }
        public List<string> txtTuKhoa { get; set; } = null;  // ?????????
        public bool ckbDieuKien { get; set; } = false; // ?????????????
        public int nudLuotToiDa { get; set; } = 50; // ?????????????
    }

    public class HDTuongTacBanBeConfig : TuongTacConfig
    {
        public bool ckbTuDongXoaNoiDung { get; set; }
        public int cbbOptionsPost { get; set; } // 0: giới hạn số bài viết tương tác, 1: giới hạn thời gian tương tác
        public int nudThoiGianFrom { get; set; }
        public int nudThoiGianTo { get; set; }
        public int nudSoLuongBanFrom { get; set; }
        public int nudSoLuongBanTo { get; set; }
    }

    public class HDTuongTacPageConfig : TuongTacConfig
    {
        public bool ckbTuDongXoaNoiDung { get; set; }
        public int cbbOptionsPost { get; set; } // 0: giới hạn số bài viết tương tác, 1: giới hạn thời gian tương tác
        public int nudThoiGianFrom { get; set; }
        public int nudThoiGianTo { get; set; }
        public int nudSoLuongProfileFrom { get; set; }
        public int nudSoLuongProfileTo { get; set; } 
        public List<string> txtId { get; set; }
        public bool ckbLikePage { get; set; } = false; // ??????????
    }

    public class HDTuongTacProfileConfig : TuongTacConfig
    {
        public bool ckbTuDongXoaNoiDung { get; set; }
        public int cbbOptionsPost { get; set; } // 0: giới hạn số bài viết tương tác, 1: giới hạn thời gian tương tác
        public int nudThoiGianFrom { get; set; }
        public int nudThoiGianTo { get; set; }
        public bool ckbPublicPost { get; set; }
        public bool ckbPrivatePost { get; set; }
        public List<string> txtId { get; set; }
        public int cbbDoiTuong { get; set; } // 0: tương tác tường của tài khoản, 1: tương tác profile chỉ định
        public int nudSoLuongProfileFrom { get; set; }
        public int nudSoLuongProfileTo { get; set; }
    }

    public class HDKetBanTheoTuKhoaConfig : CommonConfig
    {
        public int nudSoLuongKetBanMoiTuKhoaFrom { get; set; }
        public int nudSoLuongKetBanMoiTuKhoaTo { get; set; }
        public List<string> txtTuKhoa { get; set; }
    }

    public class HDKetBanTepUidConfig : TuongTacConfig
    {
        public int nudPostFrom { get; set; } // số lượng bạn from
        public int nudPostTo { get; set; } // số lượng bạn to
        public bool ckbBinhLuanNhieuLan { get; set; } = false; // ????????????????
        public int nudBinhLuanNhieuLanDelayFrom { get; set; } = 10; // ????????????????
        public int nudBinhLuanNhieuLanDelayTo { get; set; } = 10;// ????????????????
        public int nudBinhLuanNhieuLanFrom { get; set; } = 1;  // ????????????????
        public int nudBinhLuanNhieuLanTo { get; set; } = 1;// ????????????????
        public bool ckbTagNeuBat { get; set; } = false;// ????????????????
        public int typeTag { get; set; } = 0; // ????????????????

        public List<string> txtUid { get; set; }
        public bool ckbTuongTacPost { get; set; }
        public bool ckbTuDongXoaUid { get; set; }

    }

    public class HDKetBanGoiYConfig : CommonConfig
    {
        public int nudDelayCheck { get; set; } = 10; // ???????????????
        public bool ckbChiKetBanTenCoDau { get; set; } = false; // ???????????????
        public bool ckbOnlyAddFriendWithMutualFriends { get; set; } = false; // ???????????????
        public int nudTimesWarning { get; set; } = 3; // ???????????????
    }

    public class HDXacNhanKetBanConfig : CommonConfig
    {
        public bool ckbChiKetBanTenCoDau { get; set; } = false; // ???????????????
        public bool ckbOnlyAddFriendWithMutualFriends { get; set; } = false; // ???????????????
    }

    public class HDHuyKetBanConfig : CommonConfig
    {
        public bool ckbSort { get; set; }
        public int typeSort { get; set; } // 0: danh sách bạn bè mới nhất, 1: danh sách bạn bè cũ nhất
        public int typeHuyKetBan { get; set; } // 0: hủy kết bạn ngẫu nhiên, 1: hủy kết bạn theo UID
        public List<string> txtUid { get; set; }
        public List<string> txtUidKhongHuyKetBan { get; set; }
    }

    public class HDThamGiaNhomTuKhoaConfig : CommonConfig
    {
        public  List<string> txtTuKhoa { get; set; }
        public bool ckbTuDongTraLoiCauHoi { get; set; }
        public List<string> txtCauTraLoi { get; set; }
    }

    public class HDThamGiaNhomUidConfig : CommonConfig
    {
        public List<string> txtUid { get; set; }
        public bool ckbThamGiaNhomTrungNhau { get; set; }
        public bool ckbTuDongTraLoiCauHoi { get; set; }
        public List<string> txtCauTraLoi { get; set; }
        public bool ckbTuDongXoaUid { get; set; }
    }

    public class HDThamGiaNhomGoiYConfig : CommonConfig
    {
        public bool ckbTuDongTraLoiCauHoi { get; set; }
        public List<string> txtCauTraLoi { get; set; }
    }

    public class HDRoiNhomConfig : CommonConfig
    {
        public int typeRoiNhom { get; set; } // 0: rời nhóm ngẫu nhiên, 1: rời nhóm theo điều kiện
        public bool ckbDieuKienKiemDuyet { get; set; }
        public bool ckbDieuKienThanhVien { get; set; }
        public int nudThanhVienToiDa { get; set; }
        public bool ckbDieuKienTuKhoa { get; set; }
        public List<string> txtTuKhoa { get; set; }
        public List<string> txtIDNhomGiuLai { get; set; }
        public bool ckbBackupDanhSachNhom { get; set; }
    }

    public class HDTaoNhomConfig
    {
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public List<string> txtTenNhom { get; set; }
    }

    public class HDDangBaiTuongConfig
    {
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public int nudKhoangCachFrom { get; set; }
        public int nudKhoangCachTo { get; set; }
        public bool ckbVanBan { get; set; }
        public bool ckbTaoNoiDungAI { get; set; }
        public string cbbPrompt { get; set; }
        public bool ckbUseBackground { get; set; }
        public bool ckbXoaNguyenLieuDaDung { get; set; }
        public List<string> txtNoiDung { get; set; }
        public bool ckbAnh { get; set; }
        public string txtPathAnh { get; set; }
        public int nudSoLuongAnhFrom { get; set; }
        public int nudSoLuongAnhTo { get; set; }
        public bool ckbDangLink { get; set; }
        public List<string> txtLinkShare { get; set; }
        public bool ckbXoaLink { get; set; }
        public bool ckbTagFriends { get; set; }
        public int nudSoLuongTagFrom { get; set; }
        public int nudSoLuongTagTo { get; set; }
        public bool ckbTuongTacPost { get; set; }
        public bool ckbXuatLinkBaiViet { get; set; }
        public bool ckbClickPrivacy { get; set; }
    }


    public class HDDangBaiNhomConfig
    {
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public int nudKhoangCachFrom { get; set; }
        public int nudKhoangCachTo { get; set; }
        public int typeNhom { get; set; } // 0: ngẫu nhiên nhóm đã tham gia, 1: nhóm chỉ định, 2: tạo nhóm mới
        public bool ckbChiShareNhomKKD { get; set; }
        public bool ckbUuTienShareNhomNhieuThanhVien { get; set; }
        public bool ckbBackupDanhSachNhom { get; set; }
        public bool ckbKhongShareTrungNhom { get; set; }
        public bool ckbChiShareNhomThuocDanhSach { get; set; }
        public List<string> lstNhomTuNhap { get; set; }
        public List<string> txtIdNhomChiDinh { get; set; }
        public bool ckbTuDongXoaUid { get; set; }
        public List<string> txtTenNhom { get; set; }
        public bool ckbPostAnDanh { get; set; }
        public bool ckbVanBan { get; set; }
        public bool ckbUseBackground { get; set; }
        public bool ckbXoaNguyenLieuDaDung { get; set; }
        public List<string> txtNoiDung { get; set; }
        public bool ckbTaoNoiDungAI { get; set; }
        public string cbbPrompt { get; set; }
        public bool ckbAnh { get; set; }
        public string txtPathAnh { get; set; }
        public int nudSoLuongAnhFrom { get; set; }
        public int nudSoLuongAnhTo { get; set; }
        public bool ckbDangLink { get; set; } 
        public string txtLinkShare { get; set; } 
        public bool ckbXoaLink { get; set; }
        public bool ckbEvent { get; set; }
        public string txtEvent { get; set; }
        public bool ckbXuatLinkBaiViet { get; set; }
        public bool ckbRoiNhomKiemDuyet { get; set; }
        public int nudTimeoutLoadPost { get; set; }
        public bool ckbJoinGroup { get; set; }
        public List<string> lstAnswers { get; set; }
        public bool ckbTuongTacPost { get; set; }
    }

    public class HDShareBaiConfig
    {
        public int nudDelayFrom { get; set; }
        public int nudDelayTo { get; set; }
        public bool ckbShareBaiLenTuong { get; set; }
        public int nudCountWallFrom { get; set; }
        public int nudCountWallTo { get; set; }
        public bool ckbShareBaiLenNhom { get; set; }
        public int nudCountGroupFrom { get; set; }
        public int nudCountGroupTo { get; set; }
        public bool ckbShareNhomNangCao { get; set; }
        public bool ckbChiShareNhomKKD { get; set; }
        public bool ckbUuTienShareNhomNhieuThanhVien { get; set; }
        public bool ckbBackupDanhSachNhom { get; set; }
        public bool ckbKhongShareTrungNhom { get; set; }
        public bool ckbChiShareNhomThuocDanhSach { get; set; }
        public List<string> lstNhomTuNhap { get; set; }
        public bool ckbTuDongXoaNoiDung { get; set; }
        public List<string> txtLinkChiaSe { get; set; }
        public int typeLinkShare { get; set; } // 0: livestream, 1: bài viết, 2: reel
        public bool ckbVanBan { get; set; }
        public List<string> txtNoiDung { get; set; }
        public bool ckbTuongTacTruocKhiShare { get; set; }
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public bool ckbInteract { get; set; }
        public List<int> typeReaction { get; set; } // "0123456" = "Thích, Yêu thích, Thương, Haha, Ngạc nhiên, buồn, phẫn nộ"
        public bool ckbComment { get; set; }
        public List<string> txtComment { get; set; }
        public bool ckbBinhLuanNhieuLan { get; set; }
        public int nudBinhLuanNhieuLanDelayFrom { get; set; }
        public int nudBinhLuanNhieuLanDelayTo { get; set; }
    }

    public class HDSpamBaiVietConfig
    {
        public int nudSoLuongUidFrom { get; set; }
        public int nudSoLuongUidTo { get; set; }
        public int nudSoLuongBaiVietFrom { get; set; }
        public int nudSoLuongBaiVietTo { get; set; }
        public int nudDelayFrom { get; set; }
        public int nudDelayTo { get; set; }
        public int typeID { get; set; } // 0: profile, 1: group, 2: page
        public List<string> txtUid { get; set; }
        public bool ckbSwipe { get; set; }
        public int nudCountSwipeFrom { get; set; }
        public int nudCountSwipeTo { get; set; }
        public bool ckbInteract { get; set; }
        public int nudPercentLike { get; set; }
        public List<int> typeReaction { get; set; } // "0123456" = "Thích, Yêu thích, Thương, Haha, Ngạc nhiên, buồn, phẫn nộ"
        public bool ckbShareWall { get; set; }
        public int nudPercentShareWall { get; set; }
        public bool ckbComment { get; set; }
        public int nudPercentCommentText { get; set; }
        public bool ckbReply { get; set; }
        public List<string> txtComment { get; set; }
        public bool ckbTuDongXoaUid { get; set; }
        public bool ckbAnh { get; set; }
        public int nudPercentCommentImage { get; set; }
        public string txtPathAnh { get; set; }
        public bool ckbReel { get; set; }
        public bool ckbJoinGroup { get; set; }
        public List<string> lstAnswers { get; set; }
    }

    public class HDDangReelConfig
    {
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public int nudKhoangCachFrom { get; set; }
        public int nudKhoangCachTo { get; set; }
        public bool ckbVanBan { get; set; }
        public bool ckbXoaNguyenLieuDaDung { get; set; }
        public List<string> txtNoiDung { get; set; }
        public bool ckbHashtag { get; set; }
        public List<string> txtHashtag { get; set; }
        public int nudSoHashtagFrom { get; set; }
        public int nudSoHashtagTo { get; set; }
        public string txtPathAnh { get; set; }
        public bool ckbXoaVideoDaDang { get; set; }
        public bool ckbXuatLinkReels { get; set; }
        public int typeReel { get; set; } = 0; //????????????????
        public int nudTimeOutLoadVideo { get; set; }
        public int cbbWhenTimeout { get; set; } // 0: success, 1: fail
        public bool ckbTuongTacReel { get; set; }
        public bool ckbThuMucMedia { get; set; } = false; // ???????????????
        public string txtThuMucMedia { get; set; } = ""; // ???????????????
        public bool ckbKhoNoiDung { get; set; } = false; // ???????????????
        public string txtKhoNoiDung { get; set; } = ""; // ???????????????
    }

    public class HDDangStoryConfig
    {
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public int typeDang { get; set; } // 0: đăng text, 1: đăng nhạc, 2: đăng ảnh/video
        public List<string> txtNoiDung { get; set; }
        public bool ckbUseBackgroundText { get; set; }
        public int typeBaiHat { get; set; } // 0: ngẫu nhiên, 1: chỉ định
        public List<string> txtDanhSachBaiHat { get; set; }
        public bool ckbUseBackgroundNhac { get; set; }
        public bool ckbAnh { get; set; }
        public string txtPathAnh { get; set; }
        public bool ckbXoaAnhDaDang { get; set; }
        public string txtChiDangAnhPathAnh { get; set; }
        public bool ckbChiDangAnhXoaAnhDaDang { get; set; }
        public bool ckbGanLink { get; set; }
        public List<string> txtLink { get; set; }
        public bool ckbClickPrivacy { get; set; }
        public bool ckbPinStory { get; set; }
    }

    public class HDDanhGiaPageConfig
    {
        public string txtUid { get; set; } // chỉ nhập một UID ???
        public bool ckbInteract { get; set; }
        public List<string> txtComment { get; set; }
        public bool ckbTuDongXoaNoiDung { get; set; }
    }

    public class HDBuffLikePageConfig
    {
        public int nudDelayFrom { get; set; }
        public int nudDelayTo { get; set; }
        public List<string> txtUid { get; set; }
    }

    public class HDBuffFollowUIDConfig : HDBuffLikePageConfig {}

    public class HDTuongTacBaiVietTuKhoaConfig : TuongTacConfig
    {
        public bool ckbTuDongXoaNoiDung { get; set; }
        public int cbbOptionsPost { get; set; }
        public int nudThoiGianFrom { get; set; }
        public int nudThoiGianTo { get; set; }
        public bool ckbFilter { get; set; }
        public int cbbOptionsFilter { get; set; }
        public List<string> txtTuKhoa { get; set; }
    }

    public class HDTuongTacBaiVietChiDinhConfig
    {
        public int nudSoLuongUidFrom { get; set; }
        public int nudSoLuongUidTo { get; set; }
        public List<string> txtIdPost { get; set; }
        public int nudTimeFrom { get; set; }
        public int nudTimeTo { get; set; }
        public bool ckbInteract { get; set; }
        public List<int> typeReaction { get; set; } // "0123456" = "Thích, Yêu thích, Thương, Haha, Ngạc nhiên, buồn, phẫn nộ"
        public bool ckbShareWall { get; set; }
        public bool ckbComment { get; set; }
        public bool ckbTaoNoiDungAI { get; set; }
        public string cbbPrompt { get; set; }
        public bool ckbReply { get; set; }
        public List<string> txtComment { get; set; }
        public bool ckbTuDongXoaNoiDung { get; set; }
        public bool ckbTuDongXoaLink { get; set; }
        public bool ckbTuDongXoaAnh { get; set; }
        public bool ckbDeleteComment { get; set; }
        public int nudTimeDeleteFrom { get; set; }
        public int nudTimeDeleteTo { get; set; }
        public bool ckbTag { get; set; }
        public int nudSoLuongTagFrom { get; set; } 
        public int nudSoLuongTagTo { get; set; } 
        public int cbbTuyChonTag { get; set; } // 0: ngẫu nhiên trong ds bạn, 1: ngẫu nhiên trong danh sách
        public bool ckbChiTagTenViet { get; set; } 
        public List<string> txtUidTag { get; set; } 
        public bool ckbAnh { get; set; }
        public string txtPathAnh { get; set; }
        public bool ckbTuongTacVideoTrenPost { get; set; }
        public int nudTuongTacVideoTrenPostFrom { get; set; }
        public int nudTuongTacVideoTrenPostTo { get; set; }
        public bool ckbGetPostAPI { get; set; }
        public string txtApiGetPost { get; set; }
    }

    public class HDTuongTacVideoConfig
    {
        public List<string> txtLinkVideo { get; set; }
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public bool ckbInteract { get; set; }
        public List<int> typeReaction { get; set; } // "0123456" = "Thích, Yêu thích, Thương, Haha, Ngạc nhiên, buồn, phẫn nộ"
        public bool ckbShareWall { get; set; }
        public bool ckbComment { get; set; }
        public List<string> txtComment { get; set; }
        public bool ckbBinhLuanNhieuLan { get; set; }
        public int nudBinhLuanNhieuLanDelayFrom { get; set; }
        public int nudBinhLuanNhieuLanDelayTo { get; set; }
        public bool ckbTuDongXoaNoiDung { get; set; }
    }

    public class HDMoiBanBeLikePageConfig
    {
        public List<string> txtUid { get; set; } // uid nhóm
    }

    public class HDMoiBanBeVaoNhomConfig
    {
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public int nudDelayFrom { get; set; }
        public int nudDelayTo { get; set; }
        public List<string> txtIdGroup { get; set; }
        public int typeInvite { get; set; } // 0: bạn bè gợi ý,  1: bạn bè lân cận, 2: cả hai
    }

    public class HDTuongTacReelChiDinhConfig
    {
        public int nudSoLuongUidFrom { get; set; }
        public int nudSoLuongUidTo { get; set; }
        public List<string> txtIdPost { get; set; }
        public int nudTimeFrom { get; set; }
        public int nudTimeTo { get; set; }
        public bool ckbInteract { get; set; }
        public bool ckbShareWall { get; set; }
        public bool ckbComment { get; set; }
        public List<string> txtComment { get; set; }
    }

    public class HDDongBoDanhBaConfig
    {
        public List<string> txtSdt { get; set; }
        public int nudSoLuongFrom { get; set; }
        public int nudSoLuongTo { get; set; }
        public bool ckbTuDongXoa { get; set; }
        public bool ckbAutoAddFriend { get; set; }
        public int nudSoLuongKetBanFrom { get; set; }
        public int nudSoLuongKetBanTo { get; set; }
        public int nudDelayFrom { get; set; }
        public int nudDelayTo { get; set; }
    }

    public class HDDoiMatKhauConfig
    {
        public int typeMatKhau { get; set; } // 0: ngẫu nhiên, 1: chỉ định
        public List<string> txtMatKhau { get; set; }
        public int nudTimeOut { get; set; }
        public bool ckbDangXuatThietBiCu { get; set; }
    }

    public class HDUpAvatarConfig
    {
        public string txtPathFolder { get; set; }
        public bool ckbXoaAnhDaDung { get; set; }
        public bool ckbSkipIfHave { get; set; }
        public bool ckbThemKhungAvatar { get; set; }
    }

    public class HDUpCoverConfig
    {
        public string txtPathFolder { get; set; }
        public bool ckbXoaAnhDaDung { get; set; }
        public bool ckbXoaAnh { get; set; }
    }

    public class HDXoaSdtConfig{}

    public class HDOnOff2FAConfig
    {
        public int typeOnOff2FA { get; set; } // 0: tắt, 1: bật
        public int neuDaCo2FA { get; set; } // 0: không bật, 1: giữ 2fa cũ và thêm 2fa mới, 2: xóa 2fa cũ rồi thêm 2fa mới, 3: thêm 2fa mới và xóa 2fa cũ
    }

    public class HDAddMailConfig
    {
        public bool ckbAddMail { get; set; }
        public int typeAddMail { get; set; }
        public int typeMail { get; set; } // 0: hostmail, 1: generator.email, 2: unlimitmail.com, 3: hotmail9.com, 4: 1secmail.com
        public List<string> lstHotmail { get; set; }
        public List<string> lstMailDomain { get; set; }
        public List<string> lstDomain { get; set; }
        public List<string> lstDomainUnlimitMail { get; set; }
        public List<string> lstDomainDonglaomail { get; set; }
        public List<string> lstDomain1secmail { get; set; }
        public List<string> lstDomainMailtm { get; set; }
        public List<string> lstDomainMailTempSite { get; set; }
        public int nudDelayOtp { get; set; }
        public bool ckbSetPrimaryMail { get; set; }
        public bool ckbRemoveMail { get; set; }
    }

    public class HDDoiTenConfig
    {
        public int typeDatTen { get; set; } // 0: ngẫu nhiên, 1: tự đặt
        public int typeTenRandom { get; set; } // 0: tên tiếng Việt, 1: tên ngoại
        public int typeTenTuDat { get; set; } // 0: mix họ, đệm, tên; 1: họ tên
        public List<string> lstHo { get; set; }
        public List<string> lstTenDem { get; set; }
        public List<string> lstTen { get; set; }
        public List<string> lstHoTen { get; set; }
        public bool ckbTuDongXoaNoiDung { get; set; }
    }

    public class HDCapNhatThongTinConfig
    {
        public bool ckbBio { get; set; }
        public bool ckbWork { get; set; }
        public bool ckbHighSchool { get; set; }
        public bool ckbCollege { get; set; }
        public bool ckbCurrentCity { get; set; }
        public bool ckbHometown { get; set; }
        public bool ckbRelationship { get; set; }
        public bool ckbGender { get; set; }
        public bool ckbBirthday { get; set; }
        public bool ckbOtherName { get; set; }
        public List<string> txtBio { get; set; }
        public List<string> lstWork { get; set; }
        public List<string> lstHighSchool { get; set; }
        public List<string> lstCollege { get; set; }
        public List<string> lstCurrentCity { get; set; }
        public List<string> lstHometown { get; set; }
        public List<string> lstOthersName { get; set; }
        public string cbbRelationship { get; set; } // ["Single", "In a relationship", "Engaged", "Married", "In a civil union", "In a domestic partnership", "In an open relationship", "It's complicated", "Separated", "Divorced", "Widowed", "Random"]
        public string cbbGender { get; set; } // ["Male", "Female", "Random"]
        public List<string> lstDay { get; set; }
        public List<string> lstMonth { get; set; }
        public List<string> lstYear { get; set; }
        public int cbbIfHaveInfo { get; set; } // 0: "Tiếp tục thêm thông tin", 1: "Không thêm thông tin nữa", 2: "Xóa thông tin cũ rồi thêm thông tin mới", 3: "Chỉ xóa thông tin cũ"
        public bool ckbSkipWhenHave { get; set; }
        public bool ckbDeleteWhenHave { get; set; }
        public bool ckbOnlyDelete { get; set; }
    }

    public class HDDangXuatThietBiCuConfig { }

}

