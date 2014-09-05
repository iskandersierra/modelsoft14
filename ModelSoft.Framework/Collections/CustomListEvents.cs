using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ModelSoft.Framework.Collections
{
  [Flags]
  public enum CustomListEvents
  {
    None = 0x0000,

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    Before = 0x0001,
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    After = 0x0002,

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    EnterBase = 0x0004,
    BeforeEnter = EnterBase | Before,
    AfterEnter = EnterBase | After,
    Enter = BeforeEnter | AfterEnter,

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    LeaveBase = 0x0008,
    BeforeLeave = LeaveBase | Before,
    AfterLeave = LeaveBase | After,
    Leave = BeforeLeave | AfterLeave,

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    InsertBase = 0x0010,
    BeforeInsert = InsertBase | Before,
    AfterInsert = InsertBase | After,
    Insert = BeforeInsert | AfterInsert,

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    RemoveBase = 0x0020,
    BeforeRemove = RemoveBase | Before,
    AfterRemove = RemoveBase | After,
    Remove = BeforeRemove | AfterRemove,

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    SetBase = 0x0040,
    BeforeSet = SetBase | Before,
    AfterSet = SetBase | After,
    Set = BeforeSet | AfterSet,

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    ClearBase = 0x0080,
    BeforeClear = ClearBase | Before,
    AfterClear = ClearBase | After,
    Clear = BeforeClear | AfterClear,

    EnterLeave = Enter | Leave,

    Operations = Insert | Remove | Set | Clear,

    All = EnterLeave | Operations
  }
}
