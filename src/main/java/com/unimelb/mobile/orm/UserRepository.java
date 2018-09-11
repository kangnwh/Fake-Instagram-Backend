package com.unimelb.mobile.orm;

import org.springframework.data.jpa.repository.JpaRepository;

/**
 * UserRepository
 * <p>
 * Author Ning Kang
 * Date 10/9/18
 */


public interface UserRepository extends JpaRepository<User, Long> {

	public User findByUsername(String username);
}