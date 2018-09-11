package com.unimelb.mobile.dtos;

/**
 * UserModels
 * <p>
 * Author Ning Kang
 * Date 10/9/18
 */


public class UserModels {

	public class LoginModel{

		private String userId;

		private String password;

		public String getUserId() {
			return userId;
		}

		public void setUserId(String userId) {
			this.userId = userId;
		}

		public String getPassword() {
			return password;
		}

		public void setPassword(String password) {
			this.password = password;
		}
	}



}
