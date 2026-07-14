导入地面瓦片，草地，阴影的资源后shift 选中 同时操作  ：inspector 面板  multiple  64   point no filter  RGB32bit  apply

再分别分割

添加地形 ![image-20260714112341034](C:\Users\Lenovo\AppData\Roaming\Typora\typora-user-images\image-20260714112341034.png)





将草地的tilemap renderer  layer 设为-1 在 player 下方  不被掩盖 

- 修改玩家层数 ： 在 Sprites Renderer   Additional Settings



添加草地后要装饰物Decoration ，需要另外的图层



添加高地Elevation 注意 Tile Palette要切换图层再画画  为高地添加碰撞体（Elecation  直接添加 Tilemap Collider 2D）