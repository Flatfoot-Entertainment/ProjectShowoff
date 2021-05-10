using System.Collections;
using System.Collections.Generic;

//box factory to inherit from
public interface BoxFactory
{
    public Box CreateRandomBox();
    public Box CreateBox1();
    public Box CreateBox2();
    public Box CreateBox3();
}
