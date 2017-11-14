using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Common
{
    /// <summary>
    ///     表示业务操作结果的枚举
    /// </summary>
    public enum OperationResultType
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        succeed,

        /// <summary>
        ///     操作失败
        /// </summary>
        failed,
    }

    public class OperationResult
    {
        #region 构造函数

        public OperationResult()
        {

        }

        public OperationResult(OperationResultType type, object obj)
        {
            ResultType = type;
            ReturnObject = obj;
        }

        public OperationResult(OperationResultType type, object obj, string msg)
            : this(type, obj)
        {
            Message = msg;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 操作的结果
        /// </summary>
        public OperationResultType ResultType { get; set; }

        /// <summary>
        /// 返回的信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的实体
        /// </summary>
        public object ReturnObject { get; set; }

        #endregion
    }
}

