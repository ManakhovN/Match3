﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable {
    void Move(Vector2 destination);
    float MoveSpeed { set; get; }
}
