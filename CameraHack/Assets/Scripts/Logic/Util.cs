using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KUtil {
    public abstract class Either<L, R> { }
    public class Right<L, R> : Either<L, R> {
        public R val;

        public Right(R v) {
            val = v;
        }
    }
    public class Left<L, R> : Either<L, R> {
        public L val;

        public Left(L v) {
            val = v;
        }
    }
}
