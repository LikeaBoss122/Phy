using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpHillMeshGenerator : HillMeshGenerator
{
    private List<Vector3> _verticies = new List<Vector3>()
    {
        new Vector3(HillLength, 0, 2),          //0
        new Vector3(HillLength, 0, 0),          //1
        new Vector3(0, 0, 2),                   //2
        new Vector3(0, 0, 0)                    //3
    };

    private List<int> _triangles = new List<int>()
    {
        2, 4, 5, //left
        2, 5, 3,

        3, 0, 2, //down
        3, 1, 0
    };

    protected override void CreateShape()
    {
        float _deltaDistance = HillLength / (_levels.Length - 1);

        _verticies.AddRange(new Vector3[] { new Vector3(0, _maxHeight - _levels[0], 2),
                                        new Vector3(0, _maxHeight - _levels[0], 0) });
        VerticesInfo leftVerticesInfo = new VerticesInfo(5, 3, 9, 7);
        VerticesInfo rightVerticesInfo = new VerticesInfo(4, 2, 8, 6);

        for (int i = 1; i < _levels.Length; i++)
        {
            _verticies.AddRange(new Vector3[]
            {
                new Vector3(_deltaDistance * (i + 1), 0, 2),
                new Vector3(_deltaDistance * (i + 1), 0, 0),
                new Vector3(_deltaDistance * (i + 1), Context.MaxHeight - _levels[i], 2),
                new Vector3(_deltaDistance * (i + 1), Context.MaxHeight - _levels[i], 0)
            });

            _triangles.AddRange(new int[]
            {
                leftVerticesInfo.PreviousLowerID, leftVerticesInfo.PreviousUpperID, leftVerticesInfo.CurrentUpperID,
                leftVerticesInfo.PreviousLowerID, leftVerticesInfo.CurrentUpperID, leftVerticesInfo.CurrentLowerID,
                rightVerticesInfo.CurrentUpperID, rightVerticesInfo.PreviousUpperID, rightVerticesInfo.PreviousLowerID,
                rightVerticesInfo.CurrentLowerID, rightVerticesInfo.CurrentUpperID, rightVerticesInfo.PreviousLowerID,
                leftVerticesInfo.PreviousUpperID, rightVerticesInfo.PreviousUpperID, leftVerticesInfo.CurrentUpperID,
                rightVerticesInfo.PreviousUpperID, rightVerticesInfo.CurrentUpperID, leftVerticesInfo.CurrentUpperID
            });

            leftVerticesInfo = new VerticesInfo(leftVerticesInfo);
            rightVerticesInfo = new VerticesInfo(rightVerticesInfo);
        }

        _triangles.AddRange(new int[]
        {
            leftVerticesInfo.PreviousUpperID, rightVerticesInfo.PreviousLowerID, leftVerticesInfo.PreviousLowerID,
            leftVerticesInfo.PreviousLowerID, rightVerticesInfo.PreviousUpperID, rightVerticesInfo.PreviousLowerID
        });
    }

    protected override void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _verticies.ToArray();
        _mesh.triangles = _triangles.ToArray();
        _mesh.RecalculateNormals();
    }
}
