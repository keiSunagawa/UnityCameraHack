using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Positioning {
    // 位置(割合)
    public struct ParPosition {
        // 100 / (width / x)
        public int xp;
        // 100 / (height / y)
        public int yp;
    }
    // 位置(Unity数値)
    public struct Position {
        public float x;
        public float y;
    }
    public interface Positionable {
        void setPosition(Position pos);
    }
    public interface FaceSearcher {
        // 顔の位置(中心座標)を取得
        ParPosition search(Color32[] cs);
    }
    public interface PositionCalculater {
        // 位置割合からUniry object上ので位置を算出
        Position calc(ParPosition par);
    }
}
