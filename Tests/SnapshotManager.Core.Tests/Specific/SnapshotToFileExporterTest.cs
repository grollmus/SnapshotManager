using System.IO;
using System.Linq;
using Grollmus.TiaPortal.Model;
using NSubstitute;
using NUnit.Framework;
using SnapshotManager.Core.Specific;

namespace SnapshotManager.Core.Tests.Specific;

[TestFixture]
public class SnapshotToFileExporterTest
{
    [SetUp]
    public void SetUp()
    {
        _tiaPortalModel = Substitute.For<ITiaPortalModel>();
    }

    private ITiaPortalModel _tiaPortalModel;

    [Test]
    public void ExportSnapshots_GivenGlobalDBsAndSnapshot_ExportFailed_NoExport()
    {
        // arrange
        var mockSnapshot = Substitute.For<ISnapshot>();
        mockSnapshot.Export(Arg.Any<FileInfo>()).Returns(false);

        var mockGlobalDb = Substitute.For<IGlobalDb>();
        mockGlobalDb.GetSnapshot().Returns(mockSnapshot);

        _tiaPortalModel.GetGlobalDBs().Returns(new[] { mockGlobalDb });

        var exporter = new SnapshotToFileExporter(_tiaPortalModel);

        // act
        var actualExportsCount = exporter.ExportSnapshots();

        // assert
        Assert.AreEqual(0, actualExportsCount);
    }

    [Test]
    public void ExportSnapshots_GivenGlobalDBsAndSnapshot_ExportSuccessful_Export()
    {
        // arrange
        var mockSnapshot = Substitute.For<ISnapshot>();
        mockSnapshot.Export(Arg.Any<FileInfo>()).Returns(true);

        var mockGlobalDb = Substitute.For<IGlobalDb>();
        mockGlobalDb.GetSnapshot().Returns(mockSnapshot);

        _tiaPortalModel.GetGlobalDBs().Returns(new[] { mockGlobalDb });

        var exporter = new SnapshotToFileExporter(_tiaPortalModel);

        // act
        var actualExportsCount = exporter.ExportSnapshots();

        // assert
        Assert.AreEqual(1, actualExportsCount);
    }

    [Test]
    public void ExportSnapshots_GivenGlobalDBsNoSnapshot_NoExport()
    {
        // arrange
        var mockGlobalDb = Substitute.For<IGlobalDb>();
        mockGlobalDb.GetSnapshot().Returns((ISnapshot)null);

        _tiaPortalModel.GetGlobalDBs().Returns(new[] { mockGlobalDb });

        var exporter = new SnapshotToFileExporter(_tiaPortalModel);

        // act
        var actualExportsCount = exporter.ExportSnapshots();

        // assert
        Assert.AreEqual(0, actualExportsCount);
    }

    [Test]
    public void ExportSnapshots_NoGivenGlobalDBs_NoExport()
    {
        // arrange
        _tiaPortalModel.GetGlobalDBs().Returns(Enumerable.Empty<IGlobalDb>());

        var exporter = new SnapshotToFileExporter(_tiaPortalModel);

        // act
        var actualExportsCount = exporter.ExportSnapshots();

        // assert
        Assert.AreEqual(0, actualExportsCount);
    }
}