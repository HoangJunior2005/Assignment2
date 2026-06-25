namespace LearningDocumentSystem.Common.Constants
{
    public static class AppMessages
    {
        public const string MsgInvalidFileType  = "Chỉ chấp nhận file PDF, DOCX, PPTX.";
        public const string MsgFileSizeExceeded = "File vượt quá giới hạn 50MB.";
        public const string MsgDuplicateFileName = "Tên file đã tồn tại trong hệ thống. Vui lòng đổi tên file hoặc chọn file khác.";
        public const string MsgDuplicateTitle    = "Tiêu đề tài liệu đã tồn tại trong hệ thống. Vui lòng nhập tiêu đề khác.";
        public const string MsgUploadSuccess    = "Tài liệu đã được upload và xử lý thành công.";
        public const string MsgUploadFailed     = "Upload thất bại. Vui lòng thử lại.";
        public const string MsgDeleteSuccess    = "Xóa tài liệu thành công.";
        public const string MsgNotFound         = "Không tìm thấy tài nguyên.";
        public const string MsgLoginFailed      = "Tên đăng nhập hoặc mật khẩu không đúng.";
        public const string MsgAccessDenied     = "Bạn không có quyền thực hiện thao tác này.";
    }
}
