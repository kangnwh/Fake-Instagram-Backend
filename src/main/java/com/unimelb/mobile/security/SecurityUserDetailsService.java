package com.unimelb.mobile.security;

import com.unimelb.mobile.orm.User;
import com.unimelb.mobile.orm.UserRepository;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import static java.util.Collections.emptyList;

/**
 * SecurityUserDetailsService
 * <p>
 * Author Ning Kang
 * Date 11/9/18
 */

@Service
public class SecurityUserDetailsService implements UserDetailsService {

	private UserRepository userRepository;

	public SecurityUserDetailsService(UserRepository userRepository) {
		this.userRepository = userRepository;
	}

	@Override
	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
		User applicationUser = userRepository.findByUsername(username);
		if (applicationUser == null) {
			throw new UsernameNotFoundException(username);
		}
		return applicationUser; //new User(applicationUser.getUsername(), applicationUser.getPassword(), emptyList());
	}

}
