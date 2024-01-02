using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Dto
{
    public class DtoWrapper
    {
        private PortalInsertReqDto _insertReq;
        private PortalInsertResDto _insertRes;
        private PortalValidateReqDto _validateReq;
        private PortalValidateResDto _validateRes;
        private PortalDetailReqDto _portalDetailReq;
        private BranchResDto _portalDetailRes;
        private EmpOTPReqDto _empOTPReq;
        private EmpOTPResDto _empOTPRes;    
        private VerifyOTPReqDto _verifyOTPReq;
        private VerifyOTPResDto _verifyOTPRes;
        private MessageResDto _messageRes;
        private SessionCountDto _sessionRes;

        public PortalInsertReqDto insertReq
        {
            get
            {
                if (_insertReq == null)
                {
                    _insertReq = new PortalInsertReqDto();
                }
                return _insertReq;
            }
        }

        public PortalInsertResDto insertRes
        {
            get
            {
                if (_insertRes == null)
                {
                    _insertRes = new PortalInsertResDto();
                }
                return _insertRes;
            }
        }

        public PortalValidateReqDto validateReq
        {
            get
            {
                if (_validateReq == null)
                {
                    _validateReq = new PortalValidateReqDto();
                }
                return _validateReq;
            }
        }

        public PortalValidateResDto validateRes
        {
            get
            {
                if (_validateRes == null)
                {
                    _validateRes = new PortalValidateResDto();
                }
                return _validateRes;
            }
        }

        public PortalDetailReqDto portalDetailsReq
        {
            get
            {
                if (_portalDetailReq == null)
                {
                    _portalDetailReq = new PortalDetailReqDto();
                }
                return _portalDetailReq;
            }
        }
        public BranchResDto portalDetailsRes
        {
            get
            {
                if (_portalDetailRes == null)
                {
                    _portalDetailRes = new BranchResDto();
                }
                return _portalDetailRes;
            }
        }

        public EmpOTPReqDto EmpOTPRequest
        {
            get
            {
                if (_empOTPReq == null)
                {
                    _empOTPReq = new EmpOTPReqDto();
                }
                return _empOTPReq;
            }
        }

        public EmpOTPResDto EmpOTPResponse
        {
            get
            {
                if (_empOTPRes == null)
                {
                    _empOTPRes = new EmpOTPResDto();
                }
                return _empOTPRes;
            }
        }

        public VerifyOTPReqDto VerifyOTPReq
        {
            get
            {
                if (_verifyOTPReq == null)
                {
                    _verifyOTPReq = new VerifyOTPReqDto();
                }
                return _verifyOTPReq;
            }
        }

        public VerifyOTPResDto VerifyOTPRes
        {
            get
            {
                if (_verifyOTPRes == null)
                {
                    _verifyOTPRes = new VerifyOTPResDto();
                }
                return _verifyOTPRes;
            }
        }

        public MessageResDto MessageRes
        {
            get
            {
                if (_messageRes == null)
                {
                    _messageRes = new MessageResDto();
                }
                return _messageRes;
            }
        }

        public SessionCountDto sessionCount
        {
            get
            {
                if (_sessionRes == null)
                {
                    _sessionRes = new SessionCountDto();
                }
                return _sessionRes;
            }
        }


    }


}
