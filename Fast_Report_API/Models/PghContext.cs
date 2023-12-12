using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fast_Report_API.Models;

public partial class PghContext : DbContext
{
    public PghContext()
    {
    }

    public PghContext(DbContextOptions<PghContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessTbl> AccessTbls { get; set; }

    public virtual DbSet<AttachmentTbl> AttachmentTbls { get; set; }

    public virtual DbSet<AuditTrailTbl> AuditTrailTbls { get; set; }

    public virtual DbSet<AuthDetailsTbl> AuthDetailsTbls { get; set; }

    public virtual DbSet<AuthorizationTbl> AuthorizationTbls { get; set; }

    public virtual DbSet<CommoditiesTbl> CommoditiesTbls { get; set; }

    public virtual DbSet<DepartmentsTbl> DepartmentsTbls { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<ModeOfProcurementTbl> ModeOfProcurementTbls { get; set; }

    public virtual DbSet<NoaDetailsTbl> NoaDetailsTbls { get; set; }

    public virtual DbSet<NoaTbl> NoaTbls { get; set; }

    public virtual DbSet<PerformanceBondTbl> PerformanceBondTbls { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<PositionTbl> PositionTbls { get; set; }

    public virtual DbSet<PurchaseOrdersTbl> PurchaseOrdersTbls { get; set; }

    public virtual DbSet<ReceiptTbl> ReceiptTbls { get; set; }

    public virtual DbSet<SeriesTbl> SeriesTbls { get; set; }

    public virtual DbSet<StatusTbl> StatusTbls { get; set; }

    public virtual DbSet<SuppliersTbl> SuppliersTbls { get; set; }

    public virtual DbSet<TermsAndConditionTbl> TermsAndConditionTbls { get; set; }

    public virtual DbSet<UomTbl> UomTbls { get; set; }

    public virtual DbSet<UsersTbl> UsersTbls { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessTbl>(entity =>
        {
            entity.HasKey(e => e.AccessId).HasName("PRIMARY");

            entity.ToTable("access_tbl");

            entity.Property(e => e.AccessId).HasColumnName("access_ID");
            entity.Property(e => e.AccessName)
                .HasMaxLength(100)
                .HasColumnName("access_name");
        });

        modelBuilder.Entity<AttachmentTbl>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PRIMARY");

            entity.ToTable("attachment_tbl");

            entity.HasIndex(e => e.NoaId, "attachment_tbl_noa_id_foreign");

            entity.Property(e => e.AttachmentId).HasColumnName("attachment_ID");
            entity.Property(e => e.Attachment)
                .HasMaxLength(500)
                .HasColumnName("attachment");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.NoaId).HasColumnName("noa_ID");
            entity.Property(e => e.Type)
                .HasMaxLength(500)
                .HasColumnName("type");

            entity.HasOne(d => d.Noa).WithMany(p => p.AttachmentTbls)
                .HasForeignKey(d => d.NoaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attachment_tbl_noa_id_foreign");
        });

        modelBuilder.Entity<AuditTrailTbl>(entity =>
        {
            entity.HasKey(e => e.AuditTrialId).HasName("PRIMARY");

            entity.ToTable("audit_trail_tbl");

            entity.HasIndex(e => e.UserId, "audit_trial_tbl_user_id_foreign");

            entity.Property(e => e.AuditTrialId).HasColumnName("audit_trial_ID");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .HasColumnName("ip_address");
            entity.Property(e => e.Metadata)
                .HasMaxLength(500)
                .HasColumnName("metadata");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_ID");

            entity.HasOne(d => d.User).WithMany(p => p.AuditTrailTbls)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("audit_trial_tbl_user_id_foreign");
        });

        modelBuilder.Entity<AuthDetailsTbl>(entity =>
        {
            entity.HasKey(e => e.AuthDetailId).HasName("PRIMARY");

            entity.ToTable("auth_details_tbl");

            entity.HasIndex(e => e.AccessId, "auth_details_tbl_access_id_foreign");

            entity.HasIndex(e => e.AuthId, "auth_details_tbl_auth_id_foreign");

            entity.Property(e => e.AuthDetailId).HasColumnName("auth_detail_ID");
            entity.Property(e => e.AccessId).HasColumnName("access_ID");
            entity.Property(e => e.AuthId).HasColumnName("auth_ID");

            entity.HasOne(d => d.Access).WithMany(p => p.AuthDetailsTbls)
                .HasForeignKey(d => d.AccessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_details_tbl_access_id_foreign");

            entity.HasOne(d => d.Auth).WithMany(p => p.AuthDetailsTbls)
                .HasForeignKey(d => d.AuthId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_details_tbl_auth_id_foreign");
        });

        modelBuilder.Entity<AuthorizationTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("authorization_tbl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AuthorizationName)
                .HasMaxLength(50)
                .HasColumnName("authorization_name");
        });

        modelBuilder.Entity<CommoditiesTbl>(entity =>
        {
            entity.HasKey(e => e.CommoditiesId).HasName("PRIMARY");

            entity.ToTable("commodities_tbl");

            entity.Property(e => e.CommoditiesId).HasColumnName("commodities_ID");
            entity.Property(e => e.Commodities)
                .HasMaxLength(50)
                .HasColumnName("commodities");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.PersonInCharge)
                .HasMaxLength(50)
                .HasColumnName("person_in_charge");
            entity.Property(e => e.Prefix)
                .HasMaxLength(50)
                .HasColumnName("prefix");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("unit");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<DepartmentsTbl>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PRIMARY");

            entity.ToTable("departments_tbl");

            entity.HasIndex(e => e.AuthorizationsId, "departments_tbl_authorizations_id_foreign");

            entity.HasIndex(e => e.StatusId, "departments_tbl_status_id_foreign");

            entity.Property(e => e.DepartmentId).HasColumnName("department_ID");
            entity.Property(e => e.AuthorizationsId).HasColumnName("authorizations_ID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(250)
                .HasColumnName("department_name");
            entity.Property(e => e.StatusId).HasColumnName("status_ID");

            entity.HasOne(d => d.Authorizations).WithMany(p => p.DepartmentsTbls)
                .HasForeignKey(d => d.AuthorizationsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("departments_tbl_authorizations_id_foreign");

            entity.HasOne(d => d.Status).WithMany(p => p.DepartmentsTbls)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("departments_tbl_status_id_foreign");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("migrations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Batch).HasColumnName("batch");
            entity.Property(e => e.Migration1)
                .HasMaxLength(255)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<ModeOfProcurementTbl>(entity =>
        {
            entity.HasKey(e => e.ModeId).HasName("PRIMARY");

            entity.ToTable("mode_of_procurement_tbl");

            entity.Property(e => e.ModeId).HasColumnName("mode_ID");
            entity.Property(e => e.ModeDescription)
                .HasMaxLength(500)
                .HasColumnName("mode_description");
            entity.Property(e => e.ModeName)
                .HasMaxLength(50)
                .HasColumnName("mode_name");
        });

        modelBuilder.Entity<NoaDetailsTbl>(entity =>
        {
            entity.HasKey(e => e.NoaDetailsId).HasName("PRIMARY");

            entity.ToTable("noa_details_tbl");

            entity.HasIndex(e => e.NoaId, "noa_details_tbl_noa_id_foreign");

            entity.HasIndex(e => e.UomId, "noa_details_tbl_uom_id_foreign");

            entity.Property(e => e.NoaDetailsId).HasColumnName("noa_details_ID");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ItemNumber).HasColumnName("item_number");
            entity.Property(e => e.NoaId).HasColumnName("noa_ID");
            entity.Property(e => e.Quantity)
                .HasPrecision(15)
                .HasColumnName("quantity");
            entity.Property(e => e.StockPropertyNumber)
                .HasMaxLength(50)
                .HasColumnName("stock_property_number");
            entity.Property(e => e.TotalCost)
                .HasPrecision(15)
                .HasColumnName("total_cost");
            entity.Property(e => e.UnitCost)
                .HasPrecision(15)
                .HasColumnName("unit_cost");
            entity.Property(e => e.UomId).HasColumnName("uom_ID");

            entity.HasOne(d => d.Noa).WithMany(p => p.NoaDetailsTbls)
                .HasForeignKey(d => d.NoaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noa_details_tbl_noa_id_foreign");

            entity.HasOne(d => d.Uom).WithMany(p => p.NoaDetailsTbls)
                .HasForeignKey(d => d.UomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noa_details_tbl_uom_id_foreign");
        });

        modelBuilder.Entity<NoaTbl>(entity =>
        {
            entity.HasKey(e => e.NoaId).HasName("PRIMARY");

            entity.ToTable("noa_tbl");

            entity.HasIndex(e => e.AppAuthUserId, "noa_tbl_app_auth_user_id_foreign");

            entity.HasIndex(e => e.DepartmentOfficeId, "noa_tbl_department_office_id_foreign");

            entity.HasIndex(e => e.ModeOfPrecurementId, "noa_tbl_mode_of_precurement_id_foreign");

            entity.HasIndex(e => e.PerformanceId, "noa_tbl_performance_id_foreign");

            entity.HasIndex(e => e.StatusId, "noa_tbl_status_id_foreign");

            entity.HasIndex(e => e.SupplierId, "noa_tbl_supplier_id_foreign");

            entity.Property(e => e.NoaId).HasColumnName("noa_ID");
            entity.Property(e => e.AppAuthUserId).HasColumnName("app_auth_user_ID");
            entity.Property(e => e.DateAwarded)
                .HasColumnType("date")
                .HasColumnName("date_awarded");
            entity.Property(e => e.DateBid)
                .HasColumnType("date")
                .HasColumnName("date_bid");
            entity.Property(e => e.DateNeeded)
                .HasColumnType("date")
                .HasColumnName("date_needed");
            entity.Property(e => e.DepartmentOfficeId).HasColumnName("department_office_ID");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.GrandTotal)
                .HasPrecision(15)
                .HasColumnName("grand_total");
            entity.Property(e => e.GrandTotalAmountInWords)
                .HasMaxLength(500)
                .HasColumnName("grand_total_amount_in_words");
            entity.Property(e => e.ModeOfPrecurementDate)
                .HasColumnType("date")
                .HasColumnName("mode_of_precurement_date");
            entity.Property(e => e.ModeOfPrecurementId).HasColumnName("mode_of_precurement_ID");
            entity.Property(e => e.NoaContractId)
                .HasMaxLength(255)
                .HasColumnName("noa_contract_ID");
            entity.Property(e => e.NoaTitle)
                .HasMaxLength(100)
                .HasColumnName("noa_title");
            entity.Property(e => e.PerfSec).HasColumnName("perf_sec");
            entity.Property(e => e.PerfSec30)
                .HasPrecision(15)
                .HasColumnName("perf_sec_30");
            entity.Property(e => e.PerfSec5)
                .HasPrecision(15)
                .HasColumnName("perf_sec_5");
            entity.Property(e => e.PerformanceId).HasColumnName("performance_ID");
            entity.Property(e => e.PurTbl)
                .HasMaxLength(50)
                .HasColumnName("pur_tbl");
            entity.Property(e => e.StatusId).HasColumnName("status_ID");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_ID");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.AppAuthUser).WithMany(p => p.NoaTbls)
                .HasForeignKey(d => d.AppAuthUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noa_tbl_app_auth_user_id_foreign");

            entity.HasOne(d => d.DepartmentOffice).WithMany(p => p.NoaTbls)
                .HasForeignKey(d => d.DepartmentOfficeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noa_tbl_department_office_id_foreign");

            entity.HasOne(d => d.ModeOfPrecurement).WithMany(p => p.NoaTbls)
                .HasForeignKey(d => d.ModeOfPrecurementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noa_tbl_mode_of_precurement_id_foreign");

            entity.HasOne(d => d.Performance).WithMany(p => p.NoaTbls)
                .HasForeignKey(d => d.PerformanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noa_tbl_performance_id_foreign");

            entity.HasOne(d => d.Status).WithMany(p => p.NoaTbls)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noa_tbl_status_id_foreign");

            entity.HasOne(d => d.Supplier).WithMany(p => p.NoaTbls)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noa_tbl_supplier_id_foreign");
        });

        modelBuilder.Entity<PerformanceBondTbl>(entity =>
        {
            entity.HasKey(e => e.PerformanceId).HasName("PRIMARY");

            entity.ToTable("performance_bond_tbl");

            entity.Property(e => e.PerformanceId).HasColumnName("performance_ID");
            entity.Property(e => e.PerformanceBondName)
                .HasMaxLength(500)
                .HasColumnName("performance_bond_name");
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("personal_access_tokens");

            entity.HasIndex(e => e.Token, "personal_access_tokens_token_unique").IsUnique();

            entity.HasIndex(e => new { e.TokenableType, e.TokenableId }, "personal_access_tokens_tokenable_type_tokenable_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abilities)
                .HasColumnType("text")
                .HasColumnName("abilities");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp")
                .HasColumnName("expires_at");
            entity.Property(e => e.LastUsedAt)
                .HasColumnType("timestamp")
                .HasColumnName("last_used_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Token)
                .HasMaxLength(64)
                .HasColumnName("token");
            entity.Property(e => e.TokenableId).HasColumnName("tokenable_id");
            entity.Property(e => e.TokenableType).HasColumnName("tokenable_type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PositionTbl>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PRIMARY");

            entity.ToTable("position_tbl");

            entity.HasIndex(e => e.StatusId, "position_tbl_status_id_foreign");

            entity.Property(e => e.PositionId).HasColumnName("position_ID");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.IsApprovingAuthority).HasColumnName("is_approving_authority");
            entity.Property(e => e.PositionName)
                .HasMaxLength(255)
                .HasColumnName("position_name");
            entity.Property(e => e.StatusId).HasColumnName("status_ID");
        });

        modelBuilder.Entity<PurchaseOrdersTbl>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PRIMARY");

            entity.ToTable("purchase_orders_tbl");

            entity.HasIndex(e => e.CommoditiesId, "purchase_orders_tbl_commodities_id_foreign");

            entity.HasIndex(e => e.NoaId, "purchase_orders_tbl_noa_id_foreign");

            entity.HasIndex(e => e.SignatureAuthorizedOfficialId, "purchase_orders_tbl_signature_authorized_official_id_foreign");

            entity.HasIndex(e => e.SignatureChiefAccountantId, "purchase_orders_tbl_signature_chief_accountant_id_foreign");

            entity.HasIndex(e => e.StatusId, "purchase_orders_tbl_status_id_foreign");

            entity.Property(e => e.PurchaseOrderId).HasColumnName("purchase_order_ID");
            entity.Property(e => e.Amount)
                .HasPrecision(15)
                .HasColumnName("amount");
            entity.Property(e => e.CommoditiesId).HasColumnName("commodities_ID");
            entity.Property(e => e.DateNeeded)
                .HasColumnType("date")
                .HasColumnName("date_needed");
            entity.Property(e => e.DeliveryTerm)
                .HasMaxLength(255)
                .HasColumnName("delivery_term");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("date")
                .HasColumnName("encoded_date");
            entity.Property(e => e.FundCluster)
                .HasMaxLength(255)
                .HasColumnName("fund_cluster");
            entity.Property(e => e.FundsAvailable)
                .HasPrecision(15)
                .HasColumnName("funds_available");
            entity.Property(e => e.NoaId).HasColumnName("noa_ID");
            entity.Property(e => e.OrsBursDate)
                .HasColumnType("date")
                .HasColumnName("ors_burs_date");
            entity.Property(e => e.OrsBursNumber)
                .HasMaxLength(255)
                .HasColumnName("ors_burs_number");
            entity.Property(e => e.PaymentTerm)
                .HasMaxLength(255)
                .HasColumnName("payment_term");
            entity.Property(e => e.PlaceOfDelivery)
                .HasMaxLength(255)
                .HasColumnName("place_of_delivery");
            entity.Property(e => e.PurchaseOrderNumber)
                .HasMaxLength(255)
                .HasColumnName("purchase_order_number");
            entity.Property(e => e.SignatureAuthorizedOfficialId).HasColumnName("signature_authorized_official_ID");
            entity.Property(e => e.SignatureChiefAccountantId).HasColumnName("signature_chief_accountant_ID");
            entity.Property(e => e.StatusId).HasColumnName("status_ID");
            entity.Property(e => e.SupplierDate)
                .HasColumnType("date")
                .HasColumnName("supplier_date");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Commodities).WithMany(p => p.PurchaseOrdersTbls)
                .HasForeignKey(d => d.CommoditiesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_orders_tbl_commodities_id_foreign");

            entity.HasOne(d => d.Noa).WithMany(p => p.PurchaseOrdersTbls)
                .HasForeignKey(d => d.NoaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_orders_tbl_noa_id_foreign");

            entity.HasOne(d => d.SignatureAuthorizedOfficial).WithMany(p => p.PurchaseOrdersTblSignatureAuthorizedOfficials)
                .HasForeignKey(d => d.SignatureAuthorizedOfficialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_orders_tbl_signature_authorized_official_id_foreign");

            entity.HasOne(d => d.SignatureChiefAccountant).WithMany(p => p.PurchaseOrdersTblSignatureChiefAccountants)
                .HasForeignKey(d => d.SignatureChiefAccountantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_orders_tbl_signature_chief_accountant_id_foreign");

            entity.HasOne(d => d.Status).WithMany(p => p.PurchaseOrdersTbls)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_orders_tbl_status_id_foreign");
        });

        modelBuilder.Entity<ReceiptTbl>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PRIMARY");

            entity.ToTable("receipt_tbl");

            entity.HasIndex(e => e.NoaId, "receipt_tbl_noa_id_foreign");

            entity.Property(e => e.ReceiptId).HasColumnName("receipt_ID");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.NoaId).HasColumnName("noa_ID");
            entity.Property(e => e.ReceiptNumber)
                .HasMaxLength(50)
                .HasColumnName("receipt_number");
        });

        modelBuilder.Entity<SeriesTbl>(entity =>
        {
            entity.HasKey(e => e.SeriesId).HasName("PRIMARY");

            entity.ToTable("series_tbl");

            entity.HasIndex(e => e.StatusId, "series_tbl_status_id_foreign");

            entity.Property(e => e.SeriesId).HasColumnName("series_ID");
            entity.Property(e => e.Number)
                .HasPrecision(15)
                .HasColumnName("number");
            entity.Property(e => e.Prefix)
                .HasMaxLength(50)
                .HasColumnName("prefix");
            entity.Property(e => e.Screen)
                .HasMaxLength(50)
                .HasColumnName("screen");
            entity.Property(e => e.StatusId).HasColumnName("status_ID");
        });

        modelBuilder.Entity<StatusTbl>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("status_tbl");

            entity.Property(e => e.StatusId).HasColumnName("status_ID");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.Module)
                .HasMaxLength(50)
                .HasColumnName("module");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<SuppliersTbl>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("suppliers_tbl");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_ID");
            entity.Property(e => e.AttentionTitle)
                .HasMaxLength(50)
                .HasColumnName("attention_title");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .HasColumnName("contact");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(100)
                .HasColumnName("contact_person");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(500)
                .HasColumnName("delivery_address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.FaxNumber)
                .HasMaxLength(255)
                .HasColumnName("fax_number");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .HasColumnName("supplier_name");
            entity.Property(e => e.Tin)
                .HasMaxLength(20)
                .HasColumnName("TIN");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<TermsAndConditionTbl>(entity =>
        {
            entity.HasKey(e => e.TermAndConditionId).HasName("PRIMARY");

            entity.ToTable("terms_and_condition_tbl");

            entity.HasIndex(e => e.NoaTblId, "terms_and_condition_tbl_noa_tbl_id_foreign");

            entity.Property(e => e.TermAndConditionId).HasColumnName("term_and_condition_ID");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.NoaTblId).HasColumnName("noa_tbl_ID");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.NoaTbl).WithMany(p => p.TermsAndConditionTbls)
                .HasForeignKey(d => d.NoaTblId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("terms_and_condition_tbl_noa_tbl_id_foreign");
        });

        modelBuilder.Entity<UomTbl>(entity =>
        {
            entity.HasKey(e => e.UomId).HasName("PRIMARY");

            entity.ToTable("uom_tbl");

            entity.HasIndex(e => e.StatusId, "uom_tbl_status_id_foreign");

            entity.Property(e => e.UomId).HasColumnName("uom_ID");
            entity.Property(e => e.StatusId).HasColumnName("status_ID");
            entity.Property(e => e.UomAcronym)
                .HasMaxLength(50)
                .HasColumnName("uom_acronym");
            entity.Property(e => e.UomName)
                .HasMaxLength(50)
                .HasColumnName("uom_name");
        });

        modelBuilder.Entity<UsersTbl>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("PRIMARY");

            entity.ToTable("users_tbl");

            entity.HasIndex(e => e.AuthorizationId, "users_tbl_authorization_id_foreign");

            entity.HasIndex(e => e.DepartmentId, "users_tbl_department_id_foreign");

            entity.HasIndex(e => e.StatusId, "users_tbl_status_id_foreign");

            entity.Property(e => e.UsersId).HasColumnName("users_ID");
            entity.Property(e => e.AuthorizationId).HasColumnName("authorization_ID");
            entity.Property(e => e.DepartmentId).HasColumnName("department_ID");
            entity.Property(e => e.EncodedBy).HasColumnName("encoded_by");
            entity.Property(e => e.EncodedDate)
                .HasColumnType("datetime")
                .HasColumnName("encoded_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .HasColumnName("password");
            entity.Property(e => e.PositionId).HasColumnName("position_ID");
            entity.Property(e => e.StatusId).HasColumnName("status_ID");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
            entity.Property(e => e.Username)
                .HasMaxLength(250)
                .HasColumnName("username");

            entity.HasOne(d => d.Authorization).WithMany(p => p.UsersTbls)
                .HasForeignKey(d => d.AuthorizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_tbl_authorization_id_foreign");

            entity.HasOne(d => d.Department).WithMany(p => p.UsersTbls)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_tbl_department_id_foreign");

            entity.HasOne(d => d.Status).WithMany(p => p.UsersTbls)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_tbl_status_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
