using DataBase.Entities;

namespace QuanLyBanHang.Models
{
    public class SanPhamModel
    {
        public SanPham sanPham { get; set; }
        public ThuocTinh thuocTinhs { get; set; }
        public ThuocTinhChung thuocTinhChung { get; set; }
        public GiaTriThuocTinh tiaTriThuocTinh { get; set; }

        public IEnumerable<SanPham> SanPhams { get; set; }
        public IEnumerable<ThuocTinh> ThuocTinhs { get; set; }
        public IEnumerable<ThuocTinhChung> ThuocTinhChungs { get; set; }
        public IEnumerable<GiaTriThuocTinh> GiaTriThuocTinhs { get; set; }
    }

    public class ThuocTinhChungModel
    {
        public Guid ID { get; set; }
        public ThuocTinhChung thuocTinhChung { get; set; }
        public List<GiaTriThuocTinh> giaTriThuocTinhs { get; set; }
    }

    public class SanPhamModelAdd
    {
        public SanPham sanPham { get; set; }
        public List<ThuocTinhChungModel> ThuocTinhChungs { get; set; }
    }

    public class ThuocTinhChungModel2
    {
        public Guid ID { get; set; }
        public ThuocTinhChung thuocTinhChung { get; set; }
        public List<GiaTriThuocTinh> giaTriThuocTinhs { get; set; }
    }

    public class SanPhamModelAdd2
    {
        public SanPham sanPham { get; set; }
        public List<ThuocTinhChungModel> ThuocTinhChungs { get; set; }
    }
}
