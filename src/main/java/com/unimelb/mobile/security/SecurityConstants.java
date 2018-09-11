package com.unimelb.mobile.security;

import com.unimelb.mobile.orm.UserRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;

/**
 * SecurityConstants
 * <p>
 * Author Ning Kang
 * Date 11/9/18
 */

@Configuration
public class SecurityConstants {

	@Bean
	public BCryptPasswordEncoder bCryptPasswordEncoder() {
		return new BCryptPasswordEncoder();
	}


	public static final String SECRET = "@#$!@#RQDSF$%Y$%^&U^I*$#QEF!#^G";
	public static final long EXPIRATION_TIME = 864_000_000; // 10 days
	public static final String TOKEN_PREFIX = "Mobile H1 ";
	public static final String HEADER_STRING = "Authorization";
	public static final String SIGN_UP_URL = "/v1/users/sign-up";
}
