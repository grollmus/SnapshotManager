using Grollmus.TiaPortal.Model;
using NSubstitute;
using NUnit.Framework;
using SnapshotManager.Core.Specific;

namespace SnapshotManager.Core.Tests;

[TestFixture]
public class SnapshotManagerTest
{
    [SetUp]
    public void SetUp()
    {
        _tiaPortalModel = Substitute.For<ITiaPortalModel>();
    }

    private ITiaPortalModel _tiaPortalModel;

    [Test]
    public void GetExporter_ReturnsExpectedType()
    {
        // arrange
        var expectedExporter = typeof(SnapshotToFileExporter);
        var manager = new SnapshotManager(_tiaPortalModel);

        // act
        var actualExporter = manager.GetExporter()?.GetType();

        // assert
        Assert.AreEqual(expectedExporter, actualExporter);
    }
}